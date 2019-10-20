using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaijaStartupApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NaijaStartupApp.Helpers;
using static NaijaStartupApp.Models.NsuArgs;
using static NaijaStartupApp.Models.NsuVariables;

namespace NaijaStartupApp.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        TemporaryVariables temporaryVariables;
        GlobalVariables globalVariables;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<bool> Index(string username, string password)
        {

            temporaryVariables = new TemporaryVariables();
            globalVariables = new GlobalVariables();

            var Input = new UserRequest
            {
                EmailOrUsername = username,
                Password = password
            };
            var checkLogin = await _userService.AuthenticateAsync(Input);
            if (checkLogin.IsSuccessful)
            {
                var user = _userService.get_User_By_EmailOrUsername(Input.EmailOrUsername);
                //globalVariables = HttpContext.Session.GetObject<GlobalVariables>("GlobalVariables");
                //temporaryVariables = HttpContext.Session.GetObject<TemporaryVariables>("TemporaryVariables");
                globalVariables.userid = user.Id;
                globalVariables.RoleId = user.Role;
                globalVariables.userName = user.UserName;
                HttpContext.Session.SetObject("TemporaryVariables", temporaryVariables);
                HttpContext.Session.SetObject("GlobalVariables", globalVariables);
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<bool> SignUp(string firstName, string lastName, string email, string username, string password)
        {
            var Input = new CreateUserRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = username,
                Password = password,
                Role = "User"
            };
            var checkLogin = await _userService.CreateUserAsync(Input);
            if (checkLogin.IsSuccessful)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public IActionResult Profile()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Dashboard");
        }

    }
}