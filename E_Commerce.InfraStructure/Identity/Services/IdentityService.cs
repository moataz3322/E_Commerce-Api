using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.InfraStructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.InfraStructure.Identity.Services
{//AL sERVICE IN iNFRA s 34AN HYA AL HTT3AML M3 AL UserManger w al userManger hna w de al hyst5dmha al S al fe al app
    //(Authentication SERVICE)
  public  class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManger;

        public IdentityService(UserManager<ApplicationUser> userManger)
        {
            this.userManger = userManger;
        }
        public async Task<Result<bool>> CheckPassowrdAsync(string email, string passowrd, CancellationToken ct = default)
        {
            var user = await userManger.FindByEmailAsync(email);
            if (user == null)
            {
                return Result<bool>.Fail(Error.NotFound("User Not Found", $"User With Email{email} Not Found"));
            }
            else {
             
                var PassCheck = await userManger.CheckPasswordAsync(user, passowrd);
                return Result<bool>.Ok(PassCheck);
            }

            
        }

        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
           var user= await userManger.FindByEmailAsync(email);
            if (user==null)
            {
                return Result<IdentityUserResult>.Fail(Error.NotFound("User Not Found",$"User With Email{email} Not Found"));   
            }
            else
            {
                // da application user lazm t3mlo mapping 
                return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.DisplayName, user.UserName, user.Email));
            }
        }
    }
}
