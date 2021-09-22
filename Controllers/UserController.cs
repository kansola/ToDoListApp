using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpPost("register")]
        public IActionResult RegisterUser(CreateUserModel user)
        {
            string responseMessage = "";
            try
            {
                var response = _userService.RegisterUser(user, out  responseMessage);
                if (response)
                {
                    return Ok(

                                new
                                {
                                    isSuccesful= true,
                                    response = responseMessage

                                }
                        );
                }
                else
                {
                    return Ok(

                               new
                               {
                                   isSuccesful = false,
                                   response = responseMessage

                               }
                       );
                }
                
               
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + ": " + e.StackTrace);
            }
            
        }

        
    }
}
