using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application.Dtos.Products;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Profiles
{
    class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>().ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                                            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name));
        }





    }
}
