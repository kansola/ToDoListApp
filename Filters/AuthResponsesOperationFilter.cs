using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Filters
{
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        //public void Apply(OpenApiOperation operation, OperationFilterContext context)
        //{
        //    var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
        //        .Union(context.MethodInfo.GetCustomAttributes(true))
        //        .OfType<AuthorizeAttribute>();

        //    if (authAttributes.Any())
        //    {
        //        var securityRequirement = new OpenApiSecurityRequirement()
        //    {
        //        {
        //            // Put here you own security scheme, this one is an example
        //            new OpenApiSecurityScheme
        //            {
        //                 Name = "Authorization",
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer",
        //            BearerFormat = "JWT",
        //            In = ParameterLocation.Header,
        //            Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
        //            },
        //            new List<string>()
        //        }
        //    };
        //        operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
        //        operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        //    }
        //}

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
            var isAuthorized = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
            var allowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);

            if (!isAuthorized || allowAnonymous)
            {
                return;
            }
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Security = new List<OpenApiSecurityRequirement>();

            //Add JWT bearer type
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                }
            }
            );
        }




    }
}
