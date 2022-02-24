using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace LandonAPI.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {

        private readonly IHostEnvironment _environment;

        public JsonExceptionFilter(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (_environment.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occurred !";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
