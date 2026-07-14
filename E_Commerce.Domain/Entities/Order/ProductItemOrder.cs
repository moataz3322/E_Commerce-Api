using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Entities.Order
{//
  public  class ProductItemOrder:BaseEntity<int>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }=default!;
        public string PictureUrl { get; set; }=default!;
    }
}
