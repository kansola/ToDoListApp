using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Filters;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    //[TokenAuthenticationFilter]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpPost("add")]
        public IActionResult AddTask(CreateTaskModel task)
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Remove(0, 7);
            string responseMessage = "";
            try
            {
                var response = _taskService.AddTask(task, token,out responseMessage);
                if (response)
                {
                    return Ok(

                                    new
                                    {
                                        isSuccesful = true,
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

        [HttpPost("complete")]
        public IActionResult CompleteTask(CompleteTaskModel task)
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Remove(0, 7);
            string responseMessage = "";
            try
            {
                var response = _taskService.CompleteTask(task, token, out responseMessage);
                if (response)
                {
                    return Ok(

                                    new
                                    {
                                        isSuccesful = true,
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

        [HttpPost("update")]
        public IActionResult UpdateTask(UpdateTaskModel task)
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Remove(0, 7);
            string responseMessage = "";
            try
            {
                var response = _taskService.UpdateTask(task, token, out responseMessage);
                if (response)
                {
                    return Ok(

                                    new
                                    {
                                        isSuccesful = true,
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


        [HttpGet("all")]
        public IActionResult GetAllTasks()
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Remove(0, 7);
            string responseMessage = "";
            try
            {
                var response = _taskService.GetAllTasks( token,out responseMessage);
                return Ok(response);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + ": " + e.StackTrace);
            }

        }

        [HttpGet("all/completed")]
        public IActionResult GetAllCompletedTasks()
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Remove(0, 7);
            string responseMessage = "";
            try
            {
                var response = _taskService.GetAllCompletedTasks(token, out responseMessage);
                return Ok(response);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + ": " + e.StackTrace);
            }

        }
    }
}
