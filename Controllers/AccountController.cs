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
            try
            {
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
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<JsonResult> SignUp(string firstName, string lastName, string email, string username, string password)
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
                return Json(checkLogin);
            }
            else
            {
                return Json(checkLogin);
            }
        }
        public ActionResult all_admin()
        {
            return View(_context.User.Where((x => x.Role=="Admin"))
                .Select(x => new TemporaryVariables
                {
                    string_var0 = string.IsNullOrWhiteSpace(x.FirstName)?"": x.FirstName,
                    string_var1 = string.IsNullOrWhiteSpace(x.LastName) ? "" : x.LastName,
                    string_var2 = string.IsNullOrWhiteSpace(x.Email) ? "" : x.Email,
                    string_var3 = string.IsNullOrWhiteSpace(x.PhoneNumber) ? "" : x.PhoneNumber,
                    string_var4 = string.IsNullOrWhiteSpace(x.UserName) ? "" : x.UserName,
                    string_var5 = x.IsActive ?"Active": "InActive",
                    string_var6 = x.Id,
                    date_var0 = x.CreationTime
                }).ToList());
        }
        [HttpPost]
        public async Task<ActionResult> deactivate_admin(string Id)
        {
            var admin = _context.User.Where(x => x.IsDeleted == false && x.Id == Id).FirstOrDefault();
            if (admin != null)
            {
                admin.IsActive = false;
                admin.ModificationTime = DateTime.Now;
                admin.ModificationUserId = _globalVariables.userid;

                _context.Update(admin);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("all_admin", null, null);
        }
        public ActionResult AdminSignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AdminSignUp(TemporaryVariables admin)
        {
            var Input = new CreateUserRequest
            {
                FirstName = admin.string_var0,
                LastName = admin.string_var1,
                Email = admin.string_var2,
                UserName = admin.string_var4,
                Password = admin.string_var6,
                Role = "Admin"
            };
            var checkLogin = await _userService.CreateUserAsync(Input);
            if (checkLogin.IsSuccessful)
            {
                ViewBag.message = "Successfully Created";
                return View();
            }
            else
            {
                ViewBag.message = checkLogin.Error.FirstOrDefault();
                return View();
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
            var temp = _context.User.Where((x => x.IsActive == true && x.Id.ToString() == _globalVariables.userid))
                .Select(x => new TemporaryVariables
                {
                    string_var0 = x.FirstName,
                    string_var1 = x.LastName,
                    string_var2 = x.Email,
                    string_var3 = x.State,
                    string_var4 = x.PhoneNumber,
                    string_var5 = x.Country,
                    string_var6 = x.Address,
                    string_var9 = x.UserName
                }).FirstOrDefault();
            return View(temp);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(TemporaryVariables Input)
        {
            try
            {
                var users = _context.User.Find(_globalVariables.userid);
                if (users != null)
                {
                    users.FirstName = Input.string_var0;
                    users.LastName = Input.string_var1;
                    users.Email = Input.string_var2;
                    users.State = Input.string_var3;
                    users.PhoneNumber = Input.string_var4;
                    users.Country = Input.string_var5;
                    users.Address = Input.string_var6;
                    users.UserName = Input.string_var9;
                };
                if (!string.IsNullOrWhiteSpace(Input.string_var7) && !string.IsNullOrWhiteSpace(Input.string_var8))
                {
                    if (Input.string_var7.ToLower() != Input.string_var8.ToLower())
                    {
                        var password = await _userService.ChangePasswordAsync(users, Input.string_var7, Input.string_var8);
                        if (password.IsSuccessful)
                        {
                            _context.Update(users);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            ViewBag.Message = "An error occurred";
                        }

                    }
                    else
                    {
                        ViewBag.Message = "Password MisMatch";
                    }
                }
                else
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {

            }
            return View(Input);
        }
        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();

            return Redirect("/Index.html");
        }

    }
}