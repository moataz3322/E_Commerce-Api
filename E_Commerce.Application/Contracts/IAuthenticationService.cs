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














    }
}
