using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.InfraStructure.Identity.Services
{
    class TokenService : ITokenService
    {
        private readonly JwtSettings jwt;
        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            jwt = jwtOptions.Value;
        }
        public string CreateToken(string userId, string email, string userName, IReadOnlyList<string> roles)
        {
            //var token = new JwtSecurityToken(
            //    issuer: "MEN AL 3ML CREATE LL TOKEN DA",
            //    audience: "men al url al msmo7lo yt3aml m3 al token da",
            //    expires: "da al expire Time",
            //    claims:"DE al data bta3tk al hya user id w email w username .......",
            //    signingCredentials: "de al lazm t3rf eh al Algorothim al ant mst5dmo w kman al secret Key"


            //    );
            //Dlwt2y ant m7tag 
            //1- Claims 

            #region Create Claims
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,userId)
                ,new Claim(ClaimTypes.Email ,email)
                ,new Claim(ClaimTypes.Name,userName) ,


            };
            //hna ana b3ml claim lkol Role 3ndy
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            #endregion

            //2 signingCredentials [algorithim and sercretKey]

            //5le balk an da string mmkn ykon null 3ady w m7tag check 3ala al Length
            var sKey = jwt.SecretKey;
            if (string.IsNullOrEmpty(sKey))
            {
                //hna h3ml ah throw l Ex 34an msh h3rf akml ay 7aga
                throw new InvalidOperationException("Secret Key is not configured Or Missing");
            }
            if (sKey.Length<32)
            {
                //hna h3ml ah throw l Ex 34an msh h3rf akml ay 7aga
                throw new InvalidOperationException("Secret Key is too short");
            }
            //SigninCerditials hya al class al hya bt3ml sign ll token b al algorithim w al secret key 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sKey));

            var cerdentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //3- audince issure and expires => DOL MN AL APPSETTING


            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.ExpirationInMinutes),
                signingCredentials: cerdentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

      
    }
    public class JwtSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpirationInMinutes { get; set; } = default!;
    }
}
