using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Specifications
{

    //Low ant 3ayz t5tar al 7aga al 3ayzha mn al base Specfication a3ml object gded zy da w a5tar al prop al 3yzha bs w al ba2y by3mlo Ignore
  
    class PaginationSpecifications :BaseSpecifications<Product,int>
    {
       
        public PaginationSpecifications(ProductQueryParams queryParams) : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value) &&
(string.IsNullOrEmpty(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))// low wla condtion at722 hy7ot True f al where y3ni hygeb kolo
        
        {
            
        }
    }
}
