using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Dtos.Identity;

namespace E_Commerce.Application.Contracts
{
   public interface IIdentityServiceTest
    {
        //Hna al ana a7tgt a3ml IdentityUserResult k object 
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default);

        Task<Result<bool>> CheckPassowrdAsync(string email,string password, CancellationToken ct = default);





    }
}
