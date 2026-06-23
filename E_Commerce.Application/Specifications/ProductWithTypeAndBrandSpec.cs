using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Specifications
{//lkol class hyb2a leh specification tst5dmha 
    class ProductWithTypeAndBrandSpec : BaseSpecifications<Product,int>
    {
        //GetAll Ctor
        //da f3ln al includes aw al condtions bto3y
        //dol bm3na hl al brand w al type feh value wla law  
        //  public ProductWithTypeAndBrandSpec(int? BrandId, int? TypeId) :base(p=>(BrandId==null||p.BrandId==BrandId) && (TypeId==null|| p.TypeId==TypeId))
        public ProductWithTypeAndBrandSpec(ProductQueryParams queryParams)
            :base(p=>(!queryParams.BrandId.HasValue || p.BrandId== queryParams.BrandId.Value) && (!queryParams.TypeId.HasValue|| p.TypeId== queryParams.TypeId.Value)&&
            (string.IsNullOrEmpty(queryParams.SearchValue)|| p.Name.ToLower().Contains(queryParams.SearchValue.ToLower()) ))// low wla condtion at722 hy7ot True f al where y3ni hygeb kolo
            
            //3cases
            //BRAND ID NOT NULL=>    p.brandiD == brandid 
            //type ID NOT NULL=>    p.typeid == typeid  
            //type and brand ID NOT NULL=>    p.typeid == typeid && p.brandid==brand id

        {
            AddIncludeExpression(x => x.ProductType);
            AddIncludeExpression(x => x.ProductBrand);

            
            switch (queryParams.Sort) {
                case SortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                        break;
                case SortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case SortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                case SortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                    //da low ml2t4 ay 7aga hyt3aml m3 al id  7ta low 3ndy clusterd index
                    //aw ykon 3ndi m3loma a5le al default eh
                default:AddOrderBy(p => p.Id);
                    break;

                    
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        //ctor GetById

        //ana al hb3t ll base al Expresiion bta3 al certria 
        // w da 4akl tany ll ctor
        public ProductWithTypeAndBrandSpec(int id) : base(x=>x.Id==id)
        {
            AddIncludeExpression(x => x.ProductType);
            AddIncludeExpression(x => x.ProductBrand);
        }


        //public ProductWithTypeAndBrandSpec(string name):base(p=>p.Name==name)
        //{
        //    AddIncludeExpression(x => x.ProductType);
        //    AddIncludeExpression(x => x.ProductBrand);
        //}


    }
}
