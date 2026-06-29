using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Dtos.Basket;

namespace E_Commerce.Application.Contracts
{
  public  interface IBasketService
    {

        //Get take =>BasketId , return=> basket dto
        Task<Result<BasketDto>> GetBasketAsync(string BasketId, CancellationToken ct);

        //Post  == Create and Update basket  take Basket , =>return basket after update or create
        //TimeSpan? TimeToLive=default 
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto,TimeSpan? TimeToLive=default, CancellationToken ct=default);

        //Delete basketId , return=> bool yes or no


        Task<Result<bool>> DeleteBasket(string basketId, CancellationToken cancellationToken = default);


    }
}
