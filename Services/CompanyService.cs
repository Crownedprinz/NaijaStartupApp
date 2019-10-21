using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NaijaStartupApp.Data;
using NaijaStartupApp.Helpers;
using static NaijaStartupApp.Models.NsuArgs;
using static NaijaStartupApp.Models.NsuDtos;
using static NaijaStartupApp.Models.NsuVariables;

namespace NaijaStartupApp.Services
{
    public interface ICompanyService
    {
         int Company_Count();

    }
    public class CompanyService : ICompanyService
    {
        private IUserService _userService;
        private ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _hcontext;
        private GlobalVariables _globalVariables;
        private TemporaryVariables _temporaryVariables;
        public CompanyService(UserManager<User> userManager,
                            ApplicationDbContext context,
                            SignInManager<User> signInManager,
                            ILogger<CompanyService> logger,
                            IHttpContextAccessor hcontext,
                            IUserService userService
                            )
        {
            _context = context;
            _hcontext = hcontext;
            _userService = userService;
            _globalVariables = hcontext.HttpContext.Session.GetObject<GlobalVariables>("GlobalVariables");
            _temporaryVariables = hcontext.HttpContext.Session.GetObject<TemporaryVariables>("TemporaryVariables");
        }

        public int Company_Count()
        {
            return _context.Company_Registration.Where(x =>x.User.Id == _globalVariables.userid && x.RegCompleted ==true).Count();
        }
    }

}
