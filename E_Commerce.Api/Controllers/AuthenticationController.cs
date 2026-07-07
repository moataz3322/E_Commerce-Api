using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Identity;
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

        public async Task<ActionResult<UserDto>> Login(LoginDto login) =>ToActionResult(await authenticationService.LoginAsync(login));



        [HttpPost("loginTest")]

        public async Task<ActionResult<UserDtoTest>> login(LoginDtoTest login) => ToActionResult(await authTest.loginAsyncTest(login));


    }
}
