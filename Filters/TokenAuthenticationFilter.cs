using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Interfaces;

namespace ToDoListApp.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticateToken = (IAuthenticateService)context.HttpContext.RequestServices.GetService(typeof(IAuthenticateService));
            var result = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;
            string token = string.Empty;
            if (result)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;

                if (!authenticateToken.VerifyToken(token))
                {
                    result = false;
                }
            }
            if(!result)
            {
                context.ModelState.AddModelError("Unauthorized","You are not authorized");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }



        }

        
    }
}
