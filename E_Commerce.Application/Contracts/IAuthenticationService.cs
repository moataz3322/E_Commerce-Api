using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Dtos.Identity;

namespace E_Commerce.Application.Contracts
{
   public interface IAuthenticationService
    {
        //Login
        //Email/Pass =>Token ,email,DisplayName      => dto for it

        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);

        //return =>Email/Pass =>Token ,email,DisplayName  
        Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);

        Task<Result<bool>> CheckEmailExists(string email,CancellationToken ct=default);

        Task<Result<UserDto>> GetCurrentUser(string email, CancellationToken ct = default);
        //hna ant m7tag al identity 34an al UserManger
        Task<Result<AdressDto>> GetUserAdress(string email, CancellationToken ct = default);

        Task<Result<AdressDto>> UpsertUserAdressAsync(string email, AdressDto adressDto, CancellationToken ct);



    }
}
