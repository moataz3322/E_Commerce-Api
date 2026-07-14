using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Entities.Order
{//Product Items / spirated table

    //Kol 7aga mgm3a Hna
  public  class OrderItem:BaseEntity<int>
    {
        #region Comments
		//Dol mknhom msh hna 34an dol bymslo 3ndi al ProductInfo msh 34an al OrderItem Zat nfso
        //
        //public int ProductId { get; set; }
        //public string ProductName { get; set; }=default!;
        //public string PictureUrl { get; set; }=default!;

        //de al btmslo
        //lma ana 3mlt place ll order da hyb2a 4kl al data al feh mle4 d3wa ant 8yrto wla la2 al ms2ol 3no al ProductItemOrder k2ni wa5ed screenshot
        //lakn low ana kont rabto b al product direct ana low 8yrt feh hyt8yr f al item ana msh 3ayz kda  
	#endregion
        public ProductItemOrder Product { get; set; }
        //leh m25dt4 al price 34an low nfs al order wa7ed fe Discount w al tany la2 f hna ht5tlf
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        //ana low m7tag a3ml navigate LL order kont h7ot Al Fk Hna












    }
}
