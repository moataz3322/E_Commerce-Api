using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application.Dtos.Basket;
using E_Commerce.Domain.Entities.Basket;

namespace E_Commerce.Application.Profiles
{
    class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDto, CustomerBasket>().ReverseMap();


            //34an al Icollection 
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
          
        }
    }
}
