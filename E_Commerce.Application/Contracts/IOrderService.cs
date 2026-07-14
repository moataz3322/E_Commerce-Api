using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Dtos.Orders;
using E_Commerce.Domain.Entities.Order;

namespace E_Commerce.Application.Contracts
{
   public interface IOrderService
    {
        //Creat order
     //   OrderDto feh kol al m3lomat
        Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto ,string email, CancellationToken ct = default);

        //gylk id and email => return collection of order for this user or 1 order
        Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrderForUserAsync(string email, CancellationToken ct);
        Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailAsyncc(Guid id,string email, CancellationToken ct);


        Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodsAsync(CancellationToken ct);


    }
}
