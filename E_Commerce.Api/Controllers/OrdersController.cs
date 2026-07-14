using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace E_Commerce.Api.Controllers
{

    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        //Create=> post => body (order Details(basket Id - delevery method Id - shipingAdress -[ Email])) Authorize 
        [Authorize]
        [HttpPost]

        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto, CancellationToken ct) {


            return ToActionResult(await orderService.CreateOrderAsync(orderDto, GetEmailFromToken()));

        }






        //Get  api/orders  OrdersOf User by =>Email Auth      return => all UserOrder for loged in user
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetUserOrders(CancellationToken ct)
        {
            return ToActionResult(await orderService.GetAllOrderForUserAsync(GetEmailFromToken(), ct));
        }
        //Get api/orders/{id}    Order for user by Id+Email => UserOrder by id

        [Authorize]
        [HttpGet("{id:guid}")]

        //Id da bta3 al Order msh bta3 al User
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id, CancellationToken ct)
        {
            return ToActionResult(await orderService.GetOrderByIdAndEmailAsyncc(id, GetEmailFromToken(), ct));



        }



        //GetDevlery Method => get DelviryMethod kolha=> Return List of DeliveryMethod from Data base
        [AllowAnonymous]
        [HttpGet("deliveryMethods")]

        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetAllDelviryMethod(CancellationToken ct) {


            return ToActionResult(await orderService.GetDeliveryMethodsAsync(ct));

        }










    }
}
