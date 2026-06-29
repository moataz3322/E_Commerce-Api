using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Basket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class BasketController : ApiBaseController
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }
        //Get baseurl / api / basket /id
        //post baseurl / api / basket -> body[basketDto]
        //Delete baseurl / api / basket /id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //de al data al rag3a aslun lma ykon 200 ok
        [ProducesResponseType(typeof(BasketDto),StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken ct) 
        {
            var result =await basketService.GetBasketAsync(id, ct:ct);

            return ToActionResult(result);
        
        }


        [HttpPost]

        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket, CancellationToken ct) 
        {
           var result= await basketService.CreateOrUpdateBasketAsync(basket, ct:ct);

            return ToActionResult(result);
        
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct) {

        var result=  await  basketService.DeleteBasket(id);


            return ToActionResult(result);


        }


    }
}
