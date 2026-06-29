using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Basket;

namespace E_Commerce.Application.Dtos.Basket
{
   public class BasketDto
    {

        public string Id { get; set; } = default!;
        public ICollection<BasketItemDto> Items { get; set; } = [];


    }
}
