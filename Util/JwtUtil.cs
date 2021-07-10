using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Util
{
    public static class JwtUtil
    {
        public const string ISSUER = "https://localhost:5001/";
        public const string AUDIENCE = "https://localhost:5001/";
        public const string SECREC_KEY = "this-shoul-be-your-most-secure-key-for-your-api";

        public static Byte[] key()
        {
            return Encoding.UTF8.GetBytes(SECREC_KEY);
        }

        public static string GenerateJWT(UserLogin userLogin)
        {
            // data for el playload
            var claims = new[]{
                new Claim("mail", userLogin.Email)
                // another data for the payload
            };

            // key is a byte[] created using my secrec word
            // symetrickey for the encryption
            var symetricKey = new SymmetricSecurityKey(key());
            // algorithm used for the encryption
            var algoritmo = SecurityAlgorithms.HmacSha256;

            // credentials made with  csymetrickey and algorithm
            var credentials = new SigningCredentials(symetricKey, algoritmo);

            var token = new JwtSecurityToken(
               ISSUER,
               AUDIENCE,
               claims,
               notBefore: DateTime.Now,
               expires: DateTime.Now.AddDays(2),
               signingCredentials: credentials
            );

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenJson;
        }

    }
}