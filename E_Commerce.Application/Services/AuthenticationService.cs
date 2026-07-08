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
        private readonly ITokenService tokenService;

        public AuthenticationService(IIdentityService identityService,ITokenService tokenService)
        {
            this.identityService = identityService;
            this.tokenService = tokenService;
        }

        public async Task<Result<bool>> CheckEmailExists(string email, CancellationToken ct = default)
        {
            return await identityService.EmailExistsAsync(email,ct);
        }

        public async Task<Result<UserDto>> GetCurrentUser(string email, CancellationToken ct = default)
        {
         var IdentitYuserResult = await  identityService.FindUserByEmailAsync(email, ct);

            //Generate New Token
            var user = IdentitYuserResult.data;
            var roleResult =await identityService.GetUserRoles(user.Email);

            var token = tokenService.CreateToken(user.Id, user.Email, user.UserName, roleResult.data);
            //ant b3to kda 34an 3aml implict OP
            return new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token = token };

          //  var userInfo= new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token = token };
          //  return Result<UserDto>.Ok(userInfo);


        }

        public async Task<Result<AdressDto>> GetUserAdress(string email, CancellationToken ct = default)
        {
          return await identityService.GetUserAdressByEmailAsync(email);
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var userResult = await identityService.FindUserByEmailAsync(loginDto.Email, ct);


            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Fail(userResult.Error);
            }
            var passResult = await identityService.CheckPassowrdAsync(loginDto.Email, loginDto.Passowrd, ct);

            if (!passResult.IsSuccess)
            {
                return Result<UserDto>.Fail(passResult.Error);
            }
            //hna low al Pass 8lat
            if (!passResult.data)
            {
                return Result<UserDto>.Fail(Error.UnAuthurized("Invaild Email Or Pass"));
            }

            //De 4ayla kol al user data
            var user = userResult.data;
            //Mnen ageb al role ??????? => identity service 
            //hgeb al role al awel
           var rolesResult= await identityService.GetUserRoles(user.Email);

            var roles = rolesResult.data;
            var token = tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);

            var userInfo = new UserDto()
            {
                Email = loginDto.Email
              ,
                DisplayName = userResult.data.DisplayName,
                Token = token



            };

            return Result<UserDto>.Ok(userInfo);


        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
           
            //M7tag User Manger from Infra ==> from identityService
            var userResult = await identityService.RegisterAsync(registerDto, ct);
            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Fail(userResult.Error);
            }

            var user = userResult.data;
            var rolesResult =await identityService.GetUserRoles(user.Email);
            var roles = rolesResult.data;
            var token = tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);

            var userInfo = new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            };
            return Result<UserDto>.Ok(userInfo);

        }

        public async Task<Result<AdressDto>> UpsertUserAdressAsync(string email, AdressDto adressDto, CancellationToken ct)
        {
          return await  identityService.UpdateOrInsertUserAdressAsync(email, adressDto, ct);
        }
    }
}
