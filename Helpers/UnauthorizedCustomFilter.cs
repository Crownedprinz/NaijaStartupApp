using System;
using System.Linq;
using NaijaStartupApp.Models;
using NaijaStartupApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using static NaijaStartupApp.Models.NsuVariables;

namespace NaijaStartupApp.Helpers
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
            if (context.HttpContext.Session == null)
            {
                context.Result = new RedirectResult("~/Index.html");
                return;
            }
            var gV = context.HttpContext.Session.GetObject<GlobalVariables>("GlobalVariables");
            if (gV == null)
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
