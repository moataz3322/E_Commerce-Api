using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Basket;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Basket;

namespace E_Commerce.Application.Services
{//? 5LE balk hna msh ht3aml m3 unit of work wala igenaric 
    /// <summary>
    /// ana bt3aml m3 al redis
    /// </summary>
    class BasketService : IBasketService


    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper mapper;

        public BasketService(IBasketRepo basketRepo,IMapper mapper)
        {
            _basketRepo = basketRepo;
            this.mapper = mapper;
        }



        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto, TimeSpan? TimeToLive = null, CancellationToken ct = default)
        {
            //map basketdto to CustomerBasket
            var mapped = mapper.Map<CustomerBasket>(basketDto);
            //1 map from basket dto to cutomer basket /create profile

          var basketResult= await _basketRepo.CreateOrUpdateCustomerBasketAsync(mapped, TimeToLive,ct);

            if (basketResult == null)
            {
                return Result<BasketDto>.Fail(Error.Failure("Basket Create Failed", "Cant create or update Basket"));
            }
            else {
                return Result<BasketDto>.Ok(basketDto);
            }
        }

        public async Task<Result<bool>> DeleteBasket(string basketId, CancellationToken cancellationToken = default)
        {
         var result=  await  _basketRepo.DeleteBasketAsync(basketId);

            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("BasketDelete.failer", "Cant delete Basket"));


        }

        public async Task<Result<BasketDto>> GetBasketAsync(string BasketId, CancellationToken ct)
        {
            // map CustomerBasket  to  busketDto
          var basket= await _basketRepo.GetBasketAsync(BasketId, ct);
            var mapped= mapper.Map<BasketDto>(basket);
            return basket == null ? Result<BasketDto>.Fail(Error.NotFound("Basket Not Found")) : Result<BasketDto>.Ok(mapped);
        }
    }
}
