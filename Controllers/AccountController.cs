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
using static NaijaStartupApp.Models.NsuDtos;
using NaijaStartupApp.Data;

namespace NaijaStartupApp.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        TemporaryVariables temporaryVariables;
        GlobalVariables globalVariables;
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly GlobalVariables _globalVariables;
        private readonly TemporaryVariables _temporaryVariables;
        public AccountController(IUserService userService, ApplicationDbContext context, ILogger<AccountController> logger, IHttpContextAccessor hcontext)
        {
            _userService = userService;
            _context = context;
            _logger = logger;
            _globalVariables = hcontext.HttpContext.Session.GetObject<GlobalVariables>("GlobalVariables");
            _temporaryVariables = hcontext.HttpContext.Session.GetObject<TemporaryVariables>("TemporaryVariables");
        }
        [HttpPost]
        public async Task<bool> Index(string username, string password, bool rememberMe)
        {

            temporaryVariables = new TemporaryVariables();
            globalVariables = new GlobalVariables();

            var Input = new UserRequest
            {
                EmailOrUsername = username,
                Password = password,
                RememberMe = rememberMe,
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

        [HttpPost]
        public async Task<string> SaveContact(string fullName, string email, string phoneNumber, string message)
        {
            var Input = new Contact
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                Message = message,
            };
            try
            {
                await _context.AddAsync(Input);
                await _context.SaveChangesAsync();
                return "Successfully Submitted";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return "There was a problem while trying to submit your request, Please try again later";
            }
        }

        [HttpPost]
        public bool checkSession(string companyName)
        {
            var IsSession = false;
            if (_globalVariables == null)
                IsSession = false;
            else
            {
                if (_globalVariables.userid != null)
                {
                    IsSession = true;
                    _temporaryVariables.string_var1 = companyName;
                    HttpContext.Session.SetObject("TemporaryVariables", _temporaryVariables);

                }
                else
                {
                    IsSession = false;

                }
            }
                return IsSession;
        }
        public IActionResult Profile()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();

            return Redirect("/Index.html");
        }

    }
}