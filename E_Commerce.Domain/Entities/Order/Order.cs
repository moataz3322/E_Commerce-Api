using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Entities.Order
{
    public class Order : BaseEntity<Guid>
    {
        //da for EfcOre
        private Order()
        {
            
        }
        public Order(string buyerEmail, OrderAdress shipToAdress, ICollection<OrderItem> items, DeliveryMethod deliveryMethod, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAdress = shipToAdress;
            Items = items;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; } = default!;
        //Owned
        public OrderAdress ShipToAdress { get; set; } = default!;

        //orderitem relation

        //5lek faker ana 3aml de [] 34an al NullRef Exception
        public ICollection<OrderItem> Items { get; set; } = [];

        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public int DeliveryMethodId { get; set; }//Fk




        //[NotMapped]

        //public decimal Total { get { return SubTotal + DeliveryMethod.Cost; } }


        //al efCore msh bt5zen al method .. fe point 3n al Mapper hn3rfha
        public decimal GetTotal() => SubTotal + (DeliveryMethod?.Cost??  0 );

    }

}
