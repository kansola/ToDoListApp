using Microsoft.AspNetCore.Authorization;
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
using ToDoListApp.Filters;
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

        /// <summary>
        /// A registered user logs into the ToDo Application and when successful a token is generated, the token should be added to the authorization header above.
        /// </summary>

        [AllowAnonymous]
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


        /// <summary>
        /// User logs out of the ToDo Application
        /// </summary>

        [Authorize]
        [HttpPost("user/logout")]
        public IActionResult LogOut( )
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;

            token = token.Remove(0, 7);
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
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
