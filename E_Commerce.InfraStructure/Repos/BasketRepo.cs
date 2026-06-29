using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Basket;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace E_Commerce.InfraStructure.Repos
{
    class BasketRepo : IBasketRepo

    {//Hna ana msh bt3aml me data base wala dbcontext ana 3ayz object mn connection al redis
        private readonly IDatabase database;

        //fe 2 interface  IDistributedCache
        //IConnectionMultiplexer da byt3aml direct m3 Redis
        public BasketRepo(IConnectionMultiplexer connection)
        {
            //kda ant m3ak al data base
            database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateCustomerBasketAsync(CustomerBasket customerBasket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            //Serialize → object ➝ JSON string.

          //  Deserialize < T > → JSON string ➝ object of type T.
          
            var value=JsonSerializer.Serialize(customerBasket);
            //hna hst5dm al set low l2eto ht3ml update low la2 ht3ml create
            //redis key = guid da al mtf2en 3leh  basket Id 
            //redis calue = Customer bASKET KOLO bs 3ayzo k string

           var result=await database.StringSetAsync(customerBasket.Id, value, timeToLive ?? TimeSpan.FromDays(7));

            return result ? customerBasket : null;

        }

        public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
         return await  database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
         var basket= await database.StringGetAsync(basketId);//hwa rg3lk 3ala hy2t redis value = string w ant 3yz trg3 obj
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);


        }
    }
}
