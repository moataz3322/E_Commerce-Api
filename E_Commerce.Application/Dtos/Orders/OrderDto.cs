using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Dtos.Identity;

namespace E_Commerce.Application.Dtos.Orders
{
  public  class OrderDto
    {
        [Required]
        public string BasketId { get; set; } = default!;
        [Required]
        public int DeliveryMethodId { get; set; } = default!;

        [Required]
        //ast5dmt nfs al 4akl bs
        public AdressDto ShipToAdress { get; set; } = default!;



    }
}
