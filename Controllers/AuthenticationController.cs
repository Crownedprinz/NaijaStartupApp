using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NaijaStartupApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult auth_login()
        {
            return View();
        }
        public IActionResult auth_reconver_pw()
        {
            return View();
        }
        public IActionResult auth_register()
        {
            return View();
        }

    }
}