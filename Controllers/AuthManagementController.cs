using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Configuration;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(IOptionsMonitor<JwtConfig> optionsMonitor, IAuthenticateService authenticateService)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _authenticateService = authenticateService;
        }


        [HttpPost("user/login")]
        public IActionResult AuthenticateUser(Login login)
        {
            string responseMessage = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _authenticateService.AuthenticateUser(login,out responseMessage);
                    if (response)
                    {
                        var jwtToken = GenerateJwtToken(login);

                        _authenticateService.RegisterToken(login.Email, jwtToken);


                        return Ok(
                                     new
                                     {

                                         Success = true,
                                         Token = jwtToken

                                     }


                            );
                    }
                    else
                    {
                        return BadRequest(

                       new
                       {

                           Success = false,
                           Errors = new List<string>()
                           {
                                responseMessage
                           }

                       }


                       );
                    }

                }
                else
                {
                    return BadRequest(
                        
                        new 
                        {

                            Success = false,
                            Errors = new List<string>()
                            {
                                "Invalid Payload"
                            }
                            
                        }
                        
                        
                        );
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + ": " + e.StackTrace);
            }

        }


        [HttpPost("user/logout")]
        public IActionResult LogOut( [FromBody]string token)
        {
            string responseMessage = "";
            try
            {
                var response = _authenticateService.LogOut(token, out responseMessage);
                return Ok(responseMessage);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + ": " + e.StackTrace);
            }

        }

        private string GenerateJwtToken(Login login)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler(); //responsiblr for generating the token
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, login.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, login.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
