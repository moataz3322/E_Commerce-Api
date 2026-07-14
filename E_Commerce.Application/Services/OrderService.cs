using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Orders;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Order;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services
{
    class OrderService : IOrderService
    {
        private readonly IBasketRepo basketRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderService(IBasketRepo basketRepo , IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.basketRepo = basketRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {


            //awsl l ani a3ml creat new object mn order w 22dr a3mlo insert
            // var order= new Order()
            // w kol al m3aya hwa al BuyerEmail only

            //1- ageb al items => (OrderItem)=> Get Basket da kan redis

            var basket =await basketRepo.GetBasketAsync(orderDto.BasketId);
            if (basket==null)
            {
                return Error.NotFound($"Basket with Id {orderDto.BasketId} is not Found");
            }
            if (basket.Items.Count == 0)
            {
                return Error.Validation("Basket Is Empty",$"Basket with Id {orderDto.BasketId} is Empty");
            }
            //34an a3ml ana loop 3leh w a3ml validate
            //al satr da List<OrderItem>(basket.Items.Count) =>m3nah any b2olo ht3ml list 3la 2ad al items al mwgoda
            #region Ana 3aml dol m3 b3d 34an ykono Req w7ed ygeb kol al proucts mra w7da mn al db
            var orderItems = new List<OrderItem>(basket.Items.Count);


            var productIds = basket.Items.Select(i => i.Id).ToHashSet();
            //hna m7tag Genaric Repo
            //7wltha l dictionray al key bt3ha al id bta3 al product w ashel 3ala nafsy w yrg3 al product 3ltol
            var products = (await unitOfWork.GetRepo<Product, int>().GetAllAsync(new ProuctWithIdSpecifications(productIds), ct)).ToDictionary(c=>c.Id); 
            #endregion 
            foreach (var item in basket.Items)
            {
                //get product
                if (!products.TryGetValue(item.Id, out var Product))
                { 
                return Error.NotFound($"Product with Id {item.Id} is not Found");

                }
                orderItems.Add(new OrderItem (){
                
                   //=> da auto Generate Id ,

                    Price = Product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrder()
                    {
                        ProductId = Product.Id,
                        ProductName = Product.Name,
                        PictureUrl = Product.PictureUrl
                    }

                });

            }

            //2- ship to adress =>(orderAdress)

            var orderAddress=mapper.Map<OrderAdress>(orderDto.ShipToAdress);



            //3-Delivery method => (DeliveryMethod)

            var deliveryMethod = await unitOfWork.GetRepo<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId, ct);
            if (deliveryMethod==null)
            {
                return Error.NotFound($"Delivery Method with {orderDto.DeliveryMethodId} Not Found");
            }


            //4- SubTotal

            var subTotal=orderItems.Sum(i => i.Price * i.Quantity);


            //5-Total => auto ht3ml mn al method al 3mla bs 8lban m7tag map!





            //al order b2a gahez
            var order = new Order(email, orderAddress, orderItems, deliveryMethod, subTotal);

            unitOfWork.GetRepo<Order, Guid>().Add(order);
               var result=   await  unitOfWork.SaveChangesAsync(ct);

            if (result==0)
            {
              return  Error.Failure("Failed to Create Order");

            }
            else
            {//b3ml remoove ll basket mn al redius
               await basketRepo.DeleteBasketAsync(orderDto.BasketId,ct);

                var orderToReturn = mapper.Map<OrderToReturnDto>(order);
                return orderToReturn;
            }





        }

        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrderForUserAsync(string email, CancellationToken ct)
        {
           
          var orders= await unitOfWork.GetRepo<Order , Guid>().GetAllAsync(new OrderSpecfications(email), ct);

            if (orders.Any())
            {
                return Result<IReadOnlyList<OrderToReturnDto>>.Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
            }
            else 
            {

                return Error.NotFound("Orders Not Found", $"No Orders Found For User with Email {email}");
            
            
            
            }
           


        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodsAsync(CancellationToken ct)
        {

          var DMethods=await  unitOfWork.GetRepo<DeliveryMethod,int>().GetAllAsync(ct);

            if (DMethods.Any())
            {
                return Result<IReadOnlyList<DeliveryMethodDto>>.Ok(mapper.Map<IReadOnlyList<DeliveryMethodDto>>(DMethods));
            }
            else
            {
                return Error.NotFound("No DeliveryMethod To Return");
            }


        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailAsyncc(Guid id, string email, CancellationToken ct)
        {

            var orders = await unitOfWork.GetRepo<Order, Guid>().GetByIdAsync(new OrderSpecfications(id,email), ct);


            if (orders==null)
            {
                return Error.NotFound($"Order with Id {id} Not Found for User with Email {email}");
            }
            else
            {

                return Result<OrderToReturnDto>.Ok(mapper.Map<OrderToReturnDto>(orders));

            }








        }
    }
}
