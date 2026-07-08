using System.Security.Claims;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IAuthTest authTest;

        public AuthenticationController(IAuthenticationService authenticationService,IAuthTest authTest)
        {
            this.authenticationService = authenticationService;
            this.authTest = authTest;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<UserDto>> Login(LoginDto login) =>ToActionResult(await authenticationService.LoginAsync(login));



        [HttpPost("loginTest")]

        public async Task<ActionResult<UserDtoTest>> login(LoginDtoTest login) => ToActionResult(await authTest.loginAsyncTest(login));



        [HttpPost("register")]

      public async  Task<ActionResult<UserDto>> Register(RegisterDto registerDto ) => ToActionResult(await authenticationService.RegisterAsync(registerDto));



        //CehckEmail exists only=> true or false

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery]string email, CancellationToken ct) =>ToActionResult(await authenticationService.CheckEmailExists(email));


        // Get Cuurent User = al 3aml login dlw2ty
        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken ct) 
        {
            //de al feha kol al data al 4ylha al token 
            // var httpcontextEmail= HttpContext.User.Claims

            //getEmailFromToken() hya hya de 
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException("No Email Claim Found");

            return ToActionResult( await authenticationService.GetCurrentUser(email, ct));

        
        
        }


        //getcurrent user Adress ==> HTGEBO MN AL Claims Token
        [Authorize]
        [HttpGet("adress")]
      public  async Task<ActionResult<AdressDto>> GetUserAdress(CancellationToken ct) {

         
            return ToActionResult(await authenticationService.GetUserAdress(GetEmailFromToken()));

        
        }
        //Update Current UserAdress

        [Authorize]
        [HttpPut("adress")]

        public async Task<ActionResult<AdressDto>> UpdateOrInsertUserAdress( AdressDto adressDto ,CancellationToken ct) {

            return ToActionResult(await authenticationService.UpsertUserAdressAsync(GetEmailFromToken(), adressDto, ct));
        
        }


    }
}
