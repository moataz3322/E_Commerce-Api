using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Dtos.Identity;

namespace E_Commerce.Application.Dtos.Orders
{
  public  class OrderToReturnDto
    {
        public Guid Id { get; set; }

        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = default!;

        public AdressDto ShipToAdress { get; set; }

        public string DeliveryMethod { get; set; } = default!;

        public string OrderStatus { get; set; } = default!;

        public decimal SubTotal { get; set; } 
        public decimal DeliveryMethodCost { get; set; } = default!;
        public decimal Total { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }


    }
}
