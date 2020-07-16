using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using ContacPro_Serveur.DAL;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ContacPro_Serveur.Models;

namespace ContacPro_Serveur.Providers
{
    public class LoginProvider
    {

        // https://www.c-sharpcorner.com/article/building-api-gateway-using-ocelot-in-asp-net-core-part-two/
        //private static IConfiguration configuration;
        public static async Task<JsonResult> GetToken(ContacProDBContext _context, IConfiguration configuration,
            string email, string password)
        {
            JsonResult response = new JsonResult("");
            try
            {
                var login = await _context.Professionnels.Where(p => p.Courriel == email).FirstOrDefaultAsync<Professionnel>();

                if (login != null && password.Equals(login.Mdp))
                {
                    DateTime now = DateTime.UtcNow;

                    string secret = configuration.GetValue<string>("Audience:Secret");
                    string iss = configuration.GetValue<string>("Audience:Iss");
                    string aud = configuration.GetValue<string>("Audience:Aud");
                    var claims = new Claim[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, login.Courriel),
                    new Claim(JwtRegisteredClaimNames.Sub, login.UtilisateurID+""),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                    };

                    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,
                        ValidateIssuer = true,
                        ValidIssuer = iss,
                        ValidateAudience = true,
                        ValidAudience = aud,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,
                        //  RoleClaimType = login.Role  //// role client

                    };

                    var jwt = new JwtSecurityToken(
                        issuer: iss,
                        audience: aud,
                        claims: claims,
                        notBefore: now,
                        expires: now.Add(TimeSpan.FromMinutes(30)),
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    var responseJson = new
                    {
                        access_token = encodedJwt,
                        personne = login,
                        expires_in = (int)TimeSpan.FromMinutes(30).TotalSeconds
                    };

                    //////////////////
                    response.Value = responseJson;
                    response.StatusCode = 200;

                }
                else
                {
                    response.Value = "UnAuthorized";
                    response.StatusCode = 400;

                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                response.Value = ex.Message;
                response.StatusCode = 400;
            }


            return response;


        }
    }
    public class Audience
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}
