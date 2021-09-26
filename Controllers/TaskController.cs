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

        /// <summary>
        /// Add a task on the TODO Application
        /// </summary>
       
       
        [HttpPost("add")]
        public IActionResult AddTask(CreateTaskModel task)
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Remove(0, 7);
            string responseMessage = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _taskService.AddTask(task, token, out responseMessage);
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
        /// Complete a task on the ToDo Application
        /// </summary>

        [HttpPost("complete")]
        public IActionResult CompleteTask(CompleteTaskModel task)
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Remove(0, 7);
            string responseMessage = "";
            try
            {
                if(ModelState.IsValid)
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
        /// Update a task information on the ToDo Application
        /// </summary>

        [HttpPost("update")]
        public IActionResult UpdateTask(UpdateTaskModel task)
        {
            string token = string.Empty;
            token = HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Remove(0, 7);
            string responseMessage = "";
            try
            {
                if (ModelState.IsValid)
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
        /// Retreive all added tasks on the ToDo Application
        /// </summary>

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

        /// <summary>
        /// Retreive all completed tasks on the ToDo Application
        /// </summary>
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
