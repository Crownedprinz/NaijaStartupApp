using System;
using System.Linq;
using NaijaStartupApp.Models;
using NaijaStartupApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
namespace DangoteCustomerPortal.Handlers
{
        internal class UnauthorizedCustomFilterAttribute : Attribute, IActionFilter
    {
        private bool skipLogging = false;
        public void OnActionExecuting(ActionExecutingContext context)
        {

            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var attributes = descriptor.MethodInfo.CustomAttributes;
            if (context.HttpContext.User == null)
            {
                context.Result = new RedirectResult("~/Index.html");
                return;
            }
        }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            if (skipLogging)
            {
                return;
            }
            // do something after the action executes
        }
        }

}
