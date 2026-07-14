using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Specifications
{
    class ProuctWithIdSpecifications:BaseSpecifications<Product,int> 
    {
        //hashSet 34an msh m7tag al 7aga tkon Dublicated
        //3mlt contains 34an ant ba3tha k list
        public ProuctWithIdSpecifications(HashSet<int> PrdouctIds) : base(p => PrdouctIds.Contains(p.Id))
        {
            
        }
    }
}
