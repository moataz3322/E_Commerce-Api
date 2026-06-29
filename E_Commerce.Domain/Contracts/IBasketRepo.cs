using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Basket;

namespace E_Commerce.Domain.Contracts
{
  public  interface IBasketRepo
    {
        //get=> basket id and retutn => Customer basket  
        Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default);

        //create and update  
        Task<CustomerBasket?> CreateOrUpdateCustomerBasketAsync(CustomerBasket customerBasket,TimeSpan? timeToLive=default,CancellationToken ct=default);


        Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default);
    }
}
