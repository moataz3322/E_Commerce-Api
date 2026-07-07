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
    class AuthTestService : IAuthTest
    {
        private readonly IIdentityServiceTest identityServiceTest;

        public AuthTestService(IIdentityServiceTest identityServiceTest)
        {
            this.identityServiceTest = identityServiceTest;
        }
        public async Task<Result<UserDtoTest>> loginAsyncTest(LoginDtoTest login, CancellationToken ct = default)
        {
            //hna ana m7tag a3ml find by email w check pass bas 34an a3ml kol da m7tag UserManger w da f al infra s f h3ml Service tanya al Imp bta3ha
            //hykon mwgod f al infra w hna hst5dmha bs
          var emailCheck= await identityServiceTest.FindUserByEmailAsync(login.Email, ct);

            if (!emailCheck.IsSuccess)
            {
                return Result<UserDtoTest>.Fail(emailCheck.Error);
            }

            var passCheck = await identityServiceTest.CheckPassowrdAsync(login.Email, login.Passowrd, ct);
            if (!passCheck.IsSuccess)
            {
                return Result<UserDtoTest>.Fail(passCheck.Error);
            }

            if (!passCheck.data) 
            {
                return Result<UserDtoTest>.Fail(Error.UnAuthurized("Invaild Email Or Pass"));
            }


            var user = new UserDtoTest() { 
           Email=login.Email,
           DisplayName=emailCheck.data.DisplayName,
                Token = "Token"

            };

            return Result<UserDtoTest>.Ok(user);



        }
    }
}
