using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Brand.Controllers
{
    public class usermodel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }

    }


    [Route("api/[controller]")]
    public class Login : Controller
    {
        public string CreateJWT(usermodel user)
        {
            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Security code for authentification"));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(JwtRegisteredClaimNames.Sub, user.username),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };

            var token = new JwtSecurityToken(issuer: "domain.com", audience: "domain.com", claims: claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public usermodel Authenticate(usermodel login)
        {
            if (login.username == "test" && login.password == "abc123")
            {
                return new usermodel { username = login.username, email = "test@gmail.com" };
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] usermodel login)
        {
            return await Task.Run(() =>
            {
                IActionResult response = Unauthorized();
                usermodel user = Authenticate(login);
                if (user != null)
                {
                    response = Ok(new { token = CreateJWT(user) });
                }

                return response;
            });
        }
    }
}

