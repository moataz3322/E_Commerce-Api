using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Order;

namespace E_Commerce.Application.Specifications
{
    class OrderSpecfications :BaseSpecifications<Order,Guid>
    {

        public OrderSpecfications(string email):base(o=>o.BuyerEmail==email)
        {//dol Fk fe tabel al order 3ayzhom m3aya
            AddIncludeExpression(o => o.DeliveryMethod);
            AddIncludeExpression(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }
        public OrderSpecfications(Guid id , string email):base(o=>o.Id==id&&o.BuyerEmail==email)
        {
            AddIncludeExpression(o => o.DeliveryMethod);
            AddIncludeExpression(o => o.Items);
         
        }
    }
}
