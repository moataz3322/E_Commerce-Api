using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Identity;

namespace E_Commerce.Application.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService identityService;

        public AuthenticationService(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var userResult=await identityService.FindUserByEmailAsync(loginDto.Email, ct);


            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Fail(userResult.Error);
            }
            var passResult=await identityService.CheckPassowrdAsync(loginDto.Email, loginDto.Passowrd, ct);

            if (!passResult.IsSuccess)
            {
                return Result<UserDto>.Fail(passResult.Error);
            }
            //hna low al Pass 8lat
            if (!passResult.data)
            {
                return Result<UserDto>.Fail(Error.UnAuthurized("Invaild Email Or Pass"));
            }


          var userInfo= new UserDto() { 
            Email=loginDto.Email
            ,DisplayName=userResult.data.DisplayName,
            Token="Token"
            
               

            };

            return Result<UserDto>.Ok(userInfo);


        }
    }
}
