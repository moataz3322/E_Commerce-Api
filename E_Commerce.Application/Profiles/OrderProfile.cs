using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application.Dtos.Identity;
using E_Commerce.Application.Dtos.Orders;
using E_Commerce.Domain.Entities.Order;

namespace E_Commerce.Application.Profiles
{
    class OrderProfile:Profile
    {
        public OrderProfile()
        {

            CreateMap<AdressDto, OrderAdress>().ReverseMap();

            //Al total al Automapper hy2dr y map al value bta3tha
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(x=>x.DeliveryMethod,o=>o.MapFrom(o=>o.DeliveryMethod.ShortName))
                .ForMember(x=>x.DeliveryMethodCost,o=>o.MapFrom(o=>o.DeliveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.ProductId, o => o.MapFrom(o => o.Product.ProductId))
                .ForMember(x => x.ProductName, o => o.MapFrom(o => o.Product.ProductName))
                .ForMember(x => x.PictureUrl, o => o.MapFrom(o => o.Product.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();


        }


    }
}
