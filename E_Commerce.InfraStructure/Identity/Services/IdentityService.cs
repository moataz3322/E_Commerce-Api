using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Identity;
using E_Commerce.InfraStructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default)
        {
          
                return await userManger.FindByEmailAsync(email) is not null;
           

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

        public async Task<Result<AdressDto>> GetUserAdressByEmailAsync(string email, CancellationToken ct = default)
        {
            //Al adress msh mt5zn hna da mt5zn fe Sipirated table kda ant m7tag eggar loading mslan
            //userManger.FindByEmailAsync(email);

            //De al dbset bta3t al users
        var user=   await userManger.Users.Include(a => a.Adress).FirstOrDefaultAsync(a=>a.Email==email,ct);

            //ant htgebo mn al token f kda kda hwa mwgod al mo4kela an al adress mykon4 mwgod
            if (user?.Adress == null)
            {
                return Result<AdressDto>.Fail(Error.NotFound("Adress Not Found", $"User with Email{email} Is NOt Exists"));
            }
            var adress = user.Adress;
            return new AdressDto() {
                FirstName = adress.FirstName,
                LastName = adress.LastName,
                City = adress.City,
                Street = adress.Street,
                Country = adress.Country
            };
        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default)
        {
            //de htrg3lk al user kolo
          var user=await  userManger.FindByEmailAsync(email);
            if (user==null)
            {
                return Result<IReadOnlyList<string>>.Fail(Error.NotFound($"User With Email{email} Is Not Found "));
            }
            

            //34an tst5dm de ant m7tag al user aslun f tgeb al email al awel w hwa hygblk kol al user
         var role=  (await userManger.GetRolesAsync(user)).ToList();
             
            return Result<IReadOnlyList<string>>.Ok(role);




        }

        public async Task<Result<IdentityUserResult>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            //MUst Check For Email
            var EmailCheck = await userManger.FindByEmailAsync(registerDto.Email);
            if (EmailCheck is not null)
            {
                
                return Result<IdentityUserResult>.Fail(Error.Failure($"{registerDto.Email} Is Already Exists "));
            }

            var user = new ApplicationUser()
            { 
              UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber


            };


           var Result=await userManger.CreateAsync(user, registerDto.Password);
            var errors = Result.Errors.Select(e => new Error(e.Description,e.Code)).ToList();

            if (!Result.Succeeded)
            {
                return Result<IdentityUserResult>.Fail(errors);
            }

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.DisplayName, user.UserName, user.Email));


        }

        public async Task<Result<AdressDto>> UpdateOrInsertUserAdressAsync(string email, AdressDto adressDto, CancellationToken ct = default)
        {
            var user = await userManger.Users.Include(a => a.Adress).FirstOrDefaultAsync(a => a.Email == email, ct);

            //check for adress low msh mwgod hn3ml insert low mwgod yb2a update
            if (user?.Adress == null)
            {
                //Insert here
                user.Adress = new Adress()
                {
                    FirstName=adressDto.FirstName
                    ,LastName=adressDto.LastName
                    ,City=adressDto.City,
                    Country=adressDto.Country,
                    Street=adressDto.Street
                    


                };

            }
            else 
            {
                user.Adress.FirstName = adressDto.FirstName;
                user.Adress.LastName = adressDto.LastName;
                user.Adress.Country = adressDto.Country;
                user.Adress.City = adressDto.City;
                user.Adress.Street = adressDto.Street;
            }
            //l7d hna ant 3aml local a34an kda 3mlt call l update async

         var updateResult=  await userManger.UpdateAsync(user);

            if (updateResult.Succeeded)
            {

                return adressDto;

            }
            else {
                return Error.Failure("Failure",string.Join(';',updateResult.Errors.Select(s=>s.Description)));
            }



            
        }
    }
}
