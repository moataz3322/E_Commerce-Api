using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Dtos.Identity;

namespace E_Commerce.Application.Contracts
{// imp in Infra S 
   public interface IIdentityService
    {

        Task<Result<IdentityUserResult>>  FindUserByEmailAsync(string email, CancellationToken ct=default);

        Task<Result<bool>> CheckPassowrdAsync(string email,string passowrd ,CancellationToken ct = default);

        Task<Result<IdentityUserResult>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);

        Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default);


        //ana 3ayz check email ya true ya Flase Only a5af bdl ma ageb kol al data
        Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default);


        Task<Result<AdressDto>> GetUserAdressByEmailAsync(string email, CancellationToken ct=default);

        Task<Result<AdressDto>> UpdateOrInsertUserAdressAsync(string email, AdressDto adressDto, CancellationToken ct= default);
    }
}
