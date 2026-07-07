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
{
    class IdentityServiceTest : IIdentityServiceTest
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityServiceTest(UserManager<ApplicationUser> userManager)
        {
            
            this.userManager = userManager;
        }


        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
         var user= await  userManager.FindByEmailAsync(email); // al user manger hy4elk kol al data
            if (user == null)
            {
                return Result<IdentityUserResult>.Fail(Error.NotFound("User Not Found", $"User With Email{email} Not Found"));
            }
            else
            {

                var Mapingresult = new IdentityUserResult(user.Id,user.DisplayName,user.UserName,user.Email);
            return Result<IdentityUserResult>.Ok(Mapingresult);
            
            }
        }
        public async Task<Result<bool>> CheckPassowrdAsync(string email, string password, CancellationToken ct = default)
        {


            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result<bool>.Fail(Error.NotFound("User Not Found", $"User With Email{email} Not Found"));
            }
            else
            {

                var PassCheck = await userManager.CheckPasswordAsync(user, password);
                return Result<bool>.Ok(PassCheck);
            }




        }

        

     
    }
}
