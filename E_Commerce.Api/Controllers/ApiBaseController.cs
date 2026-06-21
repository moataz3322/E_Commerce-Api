using E_Commerce.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {

        //Dol ya hyrgo al data y hyrg3o fail
        public static ActionResult<T> ToActionResult<T>(Result<T> result)
        {

            if (result.IsSuccess)
            {
                //sUCCES 
                //OkObjectResult = ok() bs lazm a3mlo k object
                return new OkObjectResult(result.data);
            }
            else
            {
                return ToProblem(result.Error);
            }

        }



        public static ActionResult ToActionResult(Result result) 
        
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result);
            }
            else {
                return ToProblem(result.Error);
            }

        }
        // hna ana hrg3 
        protected static ObjectResult ToProblem(IReadOnlyList<Error> errors ) 
        {
                         var firstError = errors[0];


            //3mlna switch 3leh 34an msh 3arf no3 al error type
            var statusCode = firstError.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.UnAuthurized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbbiden => StatusCodes.Status403Forbidden,
                ErrorType.InvalidCredentails => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var problem = new ProblemDetails()
            {
                Status = statusCode,
                Title=firstError.Code,
                Detail = firstError.Discreption
                ,
                Extensions = { ["Errors"]=errors }
            };

            return new ObjectResult(problem) { StatusCode=statusCode};

        }







    }
}
