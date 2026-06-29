using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.Basket
{
   public  class CustomerBasket
    {

        // 34an al front mtf2 m3ak ano hykon [Guid]
        public string Id { get; set; } = default!;
        public ICollection<BasketItem> Items { get; set; } = [];

    }
}
