using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //get all products
        [HttpGet]

        public async Task<ActionResult<paginatedResult<ProductDto>>> GetAllProduct([FromQuery]ProductQueryParams param , CancellationToken ct)
        {

            var result = await _productService.GetAllProductsAsync(param, ct);
            return ToActionResult(result);
        }
        //get product by id

        [HttpGet("{id}")]
        //de badel l mslan ant ba3t id w ml2to4 f nafs al w2t al FrontEnd mtwk3 4akl mo3yn
        // al over load da bt2olo al 4akl bta3o 34an kda btrg3lo 4akl al object typeof()
        [ProducesResponseType(typeof(ProductDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]

        //b3d ma 3mlt al 3 methods bto3 al action result w at7kmt fehom lazm tms7 al Result<> l2no hyrg3lk al product
        public async Task<ActionResult<ProductDto>> GetProductById(int id, CancellationToken ct)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return ToActionResult(result);

        }

        //get all types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(int id, CancellationToken ct)
        {
            var result = await _productService.GetAllTypesAsync(ct);

            return ToActionResult(result);
            //if (result.IsSuccess)
            //{
            //    return Ok(result);
            //}
            //else { 
            //    // da low msh 3aml al result Pattren
            ////return new ProblemDetails

            //    return 
                    //}

        }
        // get all brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(int id, CancellationToken ct)
        {
            var result = await _productService.GetAllBrandsAsync(ct);
            return ToActionResult(result);

        }

    }
}
