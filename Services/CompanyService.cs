using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        Task<List<Company_Registration>> GetCompanies();
        Task<Company_Registration> GetCompanyById(Guid Id);
        int Ticket_Count();
        int Pending_Tasks();

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
            if(_globalVariables.RoleId.ToLower()=="user")
            return _context.Company_Registration.Where(x =>x.User.Id == _globalVariables.userid && x.RegCompleted ==true && x.IsDeleted==false).Count();
            else
            return _context.Company_Registration.Where(x =>x.RegCompleted ==true && x.IsDeleted==false).Count();
        }

        public async Task<List<Company_Registration>> GetCompanies()
        {
            if (_globalVariables.RoleId.ToLower() == "user")
                return await _context.Company_Registration.Where(x => x.User.Id == _globalVariables.userid && x.RegCompleted == true).ToListAsync();
            else
                return await _context.Company_Registration.Where(x => x.RegCompleted == true).ToListAsync();

        }
        public async Task<Company_Registration> GetCompanyById(Guid Id)
        {
            return  await _context.Company_Registration.Where(x => x.IsDeleted == false && x.Id == Id).FirstOrDefaultAsync();
        }
        public int Ticket_Count()
        {
            if (_globalVariables.RoleId.ToLower() == "user")
                return _context.ChatHeader.Where(x => x.User.Id == _globalVariables.userid && x.IsDeleted == false && x.IsTicket).Count();
            else
                return _context.ChatHeader.Where(x => x.IsDeleted == false && x.IsTicket).Count();

        }
        public int Pending_Tasks()
        {
            if (_globalVariables.RoleId.ToLower() == "user")
                return _context.Company_Registration.Where(x => x.IsDeleted == false && x.IsCacAvailable == true && x.User.Id.Equals(_globalVariables.userid) && x.RegCompleted == false).Count();
            else
                return _context.Company_Registration.Where((x => x.IsDeleted == false && x.IsCacAvailable == false)).Count();

        }
    }

}
