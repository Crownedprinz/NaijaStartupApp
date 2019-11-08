using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaijaStartupApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NaijaStartupApp.Data;
using NaijaStartupApp.Helpers;
using Newtonsoft.Json;
using PayStack.Net;
using static NaijaStartupApp.Models.NsuDtos;
using static NaijaStartupApp.Models.NsuVariables;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NaijaStartupApp.Controllers
{
    [UnauthorizedCustomFilter]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private AppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;
        GlobalVariables _globalVariables;
        TemporaryVariables _temporaryVariables;
        private readonly IConfiguration _configuration;
        public DashboardController(ApplicationDbContext context,
            IOptions<AppSettings> appsettings,
            IUserService userService, IHttpContextAccessor hcontext,
            ICompanyService companyService,
            IConfiguration configuration)
        {
            _context = context;
            _appSettings = appsettings.Value;
            _userService = userService;
            _companyService = companyService;
            _globalVariables = hcontext.HttpContext.Session.GetObject<GlobalVariables>("GlobalVariables");
            _temporaryVariables = hcontext.HttpContext.Session.GetObject<TemporaryVariables>("TemporaryVariables");
            this._configuration = configuration;
        }

        public async Task<ActionResult> Index()
        {
            var temp = new TemporaryVariables
            {
                int_var0 = _companyService.Company_Count(),
                int_var1 = _companyService.Ticket_Count(),
                int_var2 = _companyService.Pending_Tasks(),
            };
            return View(temp);
        }


        public ActionResult add_doctor()
        {
            return View();
        }

        public ActionResult add_member()
        {
            return View();
        }


        public ActionResult add_patient()
        {
            return View();
        }
        public ActionResult all_companies()
        {
            int count = 0;

            return View(_context.Company_Registration.Include(x => x.User).Where((x => x.IsDeleted == false && x.IsCacAvailable == true && x.User.Id.Equals(_globalVariables.userid) && x.RegCompleted == true)).OrderByDescending(s => s.CreationTime)
                .Select(x => new TemporaryVariables
                {
                    string_var0 = x.CompanyName,
                    string_var5 = x.CompanyType,
                    date_var0 = x.CreationTime,
                    string_var1 = x.FinancialYearEnd,
                    string_var2 = x.Address1,
                    string_var3 = x.Id.ToString(),
                    string_var4 = x.ApprovalStatus,
                    string_var10 = x.CacRegistrationNumber,
                    bool_var2 = x.RegCompleted,
                    bool_var0 = x.IsCacAvailable
                }).ToList());
        }
        [HttpGet]
        public async Task<ActionResult> edit_companies(string Id)
        {
            var company = await _context.Company_Registration.FindAsync(Guid.Parse(Id));
            if (company != null)
            {

            }
            return View();
        }
        [HttpDelete]
        public async Task<bool> delete_companies(string Id)
        {
            var company = await _context.Company_Registration.FindAsync(Id);
            if (company != null)
            {
                company.IsDeleted = true;
                company.ModificationTime = DateTime.Now;
                _context.Update(company);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
            return false;
        }
        public ActionResult all_doctors()
        {
            return View();
        }
        public ActionResult all_patients()
        {
            return View();
        }
        public ActionResult all_payments()
        {
            return View(_context.Payments.Include(x => x.Registration).Where((x => x.IsDeleted == false)).OrderByDescending(s => s.CreationTime)
                .Select(x => new TemporaryVariables
                {
                    string_var0 = x.Id.ToString(),
                    string_var1 = x.Registration.CompanyName,
                    string_var2 = x.PaymentType,
                    date_var0 = x.CreationTime,
                    string_var3 = x.Tax.ToString(),
                    string_var4 = x.Discount.ToString(),
                    string_var5 = x.Total.ToString(),
                }).ToList());
        }
        public ActionResult admin_companies()
        {
            return View(_context.Company_Registration.Include(x => x.Package).Include(s => s.User).Where((x => x.IsDeleted == false && x.IsCacAvailable == true && x.RegCompleted == true)).OrderByDescending(s => s.CreationTime)
                .Select(x => new TemporaryVariables
                {
                    string_var0 = x.User.FirstName + " " + x.User.LastName,
                    string_var1 = x.User.Email,
                    string_var2 = x.CompanyName,
                    string_var3 = x.CompanyType,
                    string_var4 = x.Package.PackageName,
                    string_var5 = x.ApprovalStatus,
                    date_var0 = x.CreationTime,
                    string_var6 = x.ApprovalStatus,
                    string_var7 = x.Id.ToString(),
                }).ToList());
        }
        public async Task<ActionResult> incentives()
        {

            return View(await _context.Incentives.Where(x => x.IsDeleted == false).OrderByDescending(s => s.CreationTime).
                            Select(x => new TemporaryVariables
                            {
                                int_var0 = x.Id,
                                string_var0 = x.IncentiveName,
                                string_var1 = x.Description,
                                string_var2 = "",
                                date_var0 = x.CreationTime,
                            }).ToListAsync());
        }

        public async Task<ActionResult> view_incentives(int Id)
        {
            var inc = await _context.Incentives.FindAsync(Id);
            var temp = new TemporaryVariables
            {
                string_var0 = inc.IncentiveName,
                string_var1 = inc.Description,
            };
            return View("new_incentive", temp);
        }

        public async Task<bool> delete_incentives(int Id)
        {
            var company = await _context.Incentives.FindAsync(Id);
            if (company != null)
            {
                company.IsDeleted = true;
                company.ModificationTime = DateTime.Now;
                _context.Update(company);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
            return false;
        }


        public ActionResult new_incentive()
        {
            return View();
        }

        public async Task<ActionResult> attach_incentives(string Id)
        {
            TemporaryVariables Input = new TemporaryVariables
            {
                string_var0 = Id
            };
            var get_inc = await _context.Comp_Incentives.Include(s => s.Registration).Where(x => x.Registration.Id.ToString() == Id && x.IsDeleted == false).ToListAsync();
            var list2 = (from compIn in _context.Comp_Incentives
                         join incents in _context.Incentives
                         on new { a1 = compIn.Incentive.Id } equals new { a1 = incents.Id }
                         into incents1
                         from incents2 in incents1.DefaultIfEmpty()
                         where compIn.Registration.Id.ToString() == Id && compIn.IsDeleted == false
                         select new { c1 = compIn.Incentive.Id, c2 = incents2.IncentiveName }).ToList();
            ViewBag.list2 = new SelectList(list2, "c1", "c2");

            var list1 = (from incents in _context.Incentives
                         join compIn in _context.Comp_Incentives
                         on new { a1 = incents.Id } equals new { a1 = compIn.Incentive.Id }
                         into compIn1
                         from compIn2 in compIn1.DefaultIfEmpty()
                         where incents.IsDeleted == false && incents.Id != compIn2.Incentive.Id
                         select new { c1 = incents.Id, c2 = incents.IncentiveName }).ToList();
            ViewBag.list1 = new SelectList(list1, "c1", "c2");
            return View(Input);
        }

        [HttpPost]
        public async Task<ActionResult> attach_incentives(TemporaryVariables Input, int[] snumber2)
        {

            var compInt = _context.Comp_Incentives.Where(x=>x.Registration.Id ==Guid.Parse(Input.string_var0)).ToList();
            _context.Comp_Incentives.RemoveRange(compInt);
            await _context.SaveChangesAsync();

            var comp = await _context.Company_Registration.Where(x => x.Id == Guid.Parse(Input.string_var0)).FirstOrDefaultAsync();
            
            if (snumber2 != null)
            {
                foreach (var bh in snumber2)
                {
                    var inc = await _context.Incentives.Where(x => x.Id == bh).FirstOrDefaultAsync();
                    var temp = new Comp_Incentives
                    {
                        Incentive = inc,
                        Registration = comp,
                    };
                    await _context.AddAsync(temp);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("attach_incentives",new { Id = Input.string_var0 });
        }

        [HttpPost]
        public async Task<ActionResult> new_incentive(TemporaryVariables Input)
        {
            Incentives inc = new Incentives
            {
                IncentiveName = Input.string_var0,
                Description = Input.string_var1,
                CreationTime = DateTime.Now,
                CreatorUserId = _globalVariables.userid,
            };
            try
            {
                await _context.AddAsync(inc);
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {

            }
            return View(Input);
        }

        public ActionResult unconfirmed_companies()
        {
            if (_globalVariables.RoleId == "Admin")
            {
                return View("admin_companies",_context.Company_Registration.Include(x => x.Package).Include(s => s.User).Where((x => x.IsDeleted == false && x.IsCacAvailable == false)).OrderByDescending(s => s.CreationTime)
                    .Select(x => new TemporaryVariables
                    {
                        string_var0 = x.User.FirstName + " " + x.User.LastName,
                        string_var1 = x.User.Email,
                        string_var2 = x.CompanyName,
                        string_var3 = x.CompanyType,
                        string_var4 = x.Package.PackageName,
                        string_var5 = x.ApprovalStatus,
                        date_var0 = x.CreationTime,
                        string_var6 = x.ApprovalStatus,
                        string_var7 = x.Id.ToString()
                    }).ToList());
            }
            else
            {

                return View("all_companies", _context.Company_Registration.Include(x => x.Package).Include(s => s.User).Where((x => x.IsDeleted == false && x.User.Id.Equals(_globalVariables.userid) && x.RegCompleted == false)).OrderByDescending(s => s.CreationTime)
                   .Select(x => new TemporaryVariables
                   {
                       string_var0 = x.CompanyName,
                       string_var5 = x.CompanyType,
                       date_var0 = x.CreationTime,
                       string_var1 = x.FinancialYearEnd,
                       string_var2 = x.Address1,
                       string_var3 = x.Id.ToString(),
                       string_var4 = x.ApprovalStatus,  
                       string_var10 = x.CacRegistrationNumber,
                       bool_var2 = x.RegCompleted,
                       bool_var0 = x.IsCacAvailable

                   }).ToList());
            }
        }

        [HttpPost]
        public async Task<ActionResult> approve_company(string Id)
        {
            var company = _context.Company_Registration.Where(x => x.IsDeleted == false && x.Id.ToString() == Id).FirstOrDefault();
            if (company != null)
            {
                company.ApprovalStatus = "Confirmed";
                company.ModificationTime = DateTime.Now;
                company.IsCacAvailable = true;
                company.ModificationUserId = _globalVariables.userid;

                _context.Update(company);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("admin_companies", null, null);
        }

        [HttpGet]
        public async Task<ActionResult> view_company(string Id)
        {
            var companyInfo = new TemporaryVariables();
            var company = await _context.Company_Registration.Include(x => x.Package).Include(s => s.addOnServices).Include(o => o.company_Officers).Include(x=>x.Comp_Incentives).ThenInclude(s=>s.Incentive).Where(x => x.Id.ToString() == Id).FirstOrDefaultAsync();
            if (company != null)
            {
                companyInfo = new TemporaryVariables
                {
                    string_var0 = company.CompanyName,
                    string_var1 = company.AlternateCompanyName,
                    string_var2 = company.BusinessActivity + " and " + company.SndBusinessActivity,
                    string_var3 = company.FinancialYearEnd,
                    string_var4 = company.Address1,
                    string_var5 = company.Address2,
                    string_var6 = company.CompanyCapitalCurrency,
                    int_var0 = company.NoOfSharesIssue,
                    string_var7 = company.SharePrice.ToString(),
                    string_var8 = company.ShareHolderName,
                    decimal_var0 = company.SharesAllocated,
                    string_var9 = company.CreationTime.ToString(),
                    string_var10 = company.Package.PackageName,
                    string_var11 = company.Package.CreationTime.ToString(),
                    string_var12 = company.Package.Price.ToString("#,##0.00"),
                    string_var15 = company.Id.ToString(),
                    string_var17 = company.TotalAmount.ToString("#,##0.00"),
                };
                
                if (company.company_Officers != null && company.company_Officers.Any())
                {
                    foreach (var item in company.company_Officers)
                    {
                        string table = "<table class='hover'><tbody>";
                        table += "<tr><th colspan='2' class='text-left'> OFFICER 1 - " + item.FullName + " </th></tr>";
                        table += "<tr><td width='200'>Full Name</td><td class='text-bold'>" + item.FullName + "</td></tr>";
                        table += "<tr><td>Gender</td><td colspan='2' class='text-bold'>" + item.Gender + "</td></tr>";
                        table += "<tr><td>Position</td><td class='text-bold'>" + item.Designation + "</td></tr>";
                        table += "<tr><td>ID Number</td><td class='text-bold'> " + item.Id_Number + " </td></tr>";
                        table += "<tr><td>ID Type</td><td class='text-bold'>" + item.Id_Type + "</td></tr>";
                        table += "<tr><td>Date of Birth</td><td class='text-bold'>" + item.Dob + "</td></tr>";
                        table += "<tr><td>Country of Birth</td><td class='text-bold'>" + item.Birth_Country + "</td></tr>";
                        table += "<tr><td>Nationality</td><td class='text-bold'>" + item.Nationality + "</td></tr>";
                        table += "<tr><td>Address Line 1</td><td class='text-bold'>" + item.Address1 + "</td></tr>";
                        table += "<tr><td>Address Line 2</td><td class='text-bold'>" + item.Address2 + "</td></tr>";
                        table += "<tr><td>Postcode</td><td class='text-bold'>" + item.PostalCode + "</td></tr>";
                        table += "<tr><td>Mobile Phone</td><td class='text-bold'>" + item.PostalCode + "</td></tr>";
                        table += "<tr><td>Work Phone</td><td class='text-bold'>" + item.Phone_No + "</td></tr>";
                        table += "<tr><td>Email</td><td class='text-bold'>" + item.Email + "</td></tr>";
                        if(item.Identification!=null)
                        table += "<tr><td>Proof of ID</td><td class='text-bold'><span onclick=displayPhotos('"+item.Id.ToString()+"','image') data-toggle='modal' data-target='#exampleModalCenter'>View Image</span></td></tr>";
                        if (item.CerficationOfBirth != null)
                            table += "<tr><td>Proof of Certificate</td><td class='text-bold'><span onclick=displayPhotos('" + item.Id.ToString() + "','birth') data-toggle='modal' data-target='#exampleModalCenter'>View Image</span></td></tr>";
                        if (item.Proficiency != null)
                            table += "<tr><td>Proof of Proficiency</td><td class='text-bold'><span onclick=displayPhotos('" + item.Id.ToString() + "','proficiency') data-toggle='modal' data-target='#exampleModalCenter'>View Image</span></td></tr>";
                        table += "</tbody></table>";
                        companyInfo.string_var13 += table;
                    }
                }
                if (company.addOnServices != null && company.addOnServices.Any())
                {
                    foreach (var item in company.addOnServices)
                    {
                        string officers = "<tr><td>" + item.ServiceName + "</td>";
                        officers += "<td class='text-center'>" + item.CreationTime + "</td>";
                        officers += "<td class='text-right'>" + item.Price.ToString("#,##0.00") + "</td></tr>";
                        companyInfo.string_var14 += officers;
                    }
                }
                if(company.Comp_Incentives != null && company.Comp_Incentives.Any())
                {
                    foreach (var item in company.Comp_Incentives)
                    {
                        string incentives = "<tr><td>" + item.Incentive.IncentiveName + "</td>";
                        incentives += "<td class='text-center'>" + item.Incentive.Description + "</td>";
                        incentives += "<td class='text-right'>" + item.CreationTime + "</td></tr>";
                        companyInfo.string_var16 += incentives;
                }
            }
        }

            
            return View(companyInfo);
        }

        [HttpGet]
        public async Task<string> GetBytes(string Id, string type)
        {
            string base64 = "";
            byte[] byteConvert = new byte[0];
            var officer = await _context.Company_Officers.Where(x => x.Id.ToString() == Id && x.IsDeleted == false).FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(type))
                return "";

            if (type == "image")
            {
                if (officer.Identification == null)
                    return "";
                byteConvert = officer.Identification;
                base64 = Convert.ToBase64String(byteConvert, 0, byteConvert.Length);
                return String.Format("data:image/gif;base64,{0}", base64);
            }

            if (type == "proficiency")
            {
                if (officer.Proficiency == null)
                    return "";
                byteConvert = officer.Proficiency;
                base64 = Convert.ToBase64String(byteConvert, 0, byteConvert.Length);
                return String.Format("data:image/gif;base64,{0}", base64);
            }

            if (type == "birth")
            {
                if (officer.CerficationOfBirth == null)
                    return "";
                byteConvert = officer.CerficationOfBirth;
                base64 = Convert.ToBase64String(byteConvert, 0, byteConvert.Length);
                return String.Format("data:image/gif;base64,{0}", base64);
            }
            return "";
        }

        public ActionResult all_rooms()
        {
            return View();
        }
        public ActionResult all_staff()
        {
            return View();
        }
        public async Task<ActionResult> new_company(string Id)
        {
            TemporaryVariables tempValues = new TemporaryVariables
            {
                bool_var0 = false,
                list_var0 = new List<string>()
            };
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var company = await _context.Company_Registration.FindAsync(Guid.Parse(Id));
                if (company != null)
                {
                    _temporaryVariables.string_var0 = company.Id.ToString();
                    HttpContext.Session.SetObject("TemporaryVariables", _temporaryVariables);
                    return View(tempValues = new TemporaryVariables {string_var0=company.CompanyName, string_var10= company.CompanyType, bool_var0=true,list_var0=new List<string>() });
                }
                
            }
            if (_temporaryVariables !=null)
            {
                if (_temporaryVariables.string_var1 != null)
                    tempValues.string_var0 = _temporaryVariables.string_var1;
            }

            return View(tempValues);
        }
        [HttpPost]
        public ActionResult new_company(TemporaryVariables Input)
        {
            if (ModelState.IsValid)
            {
                if (Input.list_var0 == null) Input.list_var0 = new List<string>();
                int count = 1;
                string table = "<tr><td colspan='5' class='text-left text-bold'>Similar Match</td></tr>";
                _temporaryVariables.string_var1 = Input.string_var0 + " " + Input.string_var10;
                HttpContext.Session.SetObject("TemporaryVariables", _temporaryVariables);
                var exComp = _context.Company_Registration.Where(x => x.CompanyName.Equals(Input.string_var0) && x.CompanyType.Equals(Input.string_var10) && x.IsDeleted == false && x.RegCompleted == true).FirstOrDefault();
                if (exComp != null)
                {
                    var simCopanies = _context.Company_Registration.Where(x => x.CompanyName.Contains(Input.string_var0) && x.RegCompleted == true).ToList();

                    Input.bool_var0 = false;
                    Input.bool_var1 = true;
                    Input.bool_var2 = false;
                    _temporaryVariables.string_var0 = exComp.Id.ToString();
                    HttpContext.Session.SetObject("TemporaryVariables", _temporaryVariables);
                    foreach (var item in simCopanies)
                    {
                        Input.list_var0.Add(item.CompanyName);
                        table = "<td>" + count + "</td><td class='gray-bg'>" + item.CompanyName + "</td><td>" + item.CompanyType + "</td><td class='gray-bg'>" + item.CreationTime.ToString("dd MMMM yyyy")+"</td><td>"+item.ApprovalStatus+"</td>";
                        Input.string_var2 += table;
                        count++;
                    }
                    return View(Input);
                }
                else
                {
                    exComp = _context.Company_Registration.Include(x=>x.User).Where(x => x.CompanyName.Equals(Input.string_var0) && x.CompanyType.Equals(Input.string_var10) && x.IsDeleted == false && x.RegCompleted==false && x.IsCacAvailable==false).FirstOrDefault();
                    if (exComp != null)
                    {
                        if(exComp.User.Id != _globalVariables.userid)
                            Input.bool_var1 = true;
                        else
                            Input.bool_var3 = true;
                        _temporaryVariables.string_var0 = exComp.Id.ToString();
                        HttpContext.Session.SetObject("TemporaryVariables", _temporaryVariables);
                        var simCopanies1 = _context.Company_Registration.Where(x => x.CompanyName.Contains(Input.string_var0) && x.RegCompleted == true).ToList();
                            foreach (var item in simCopanies1)
                            {
                                Input.list_var0.Add(item.CompanyName);
                                table = "<td>" + count + "</td><td class='gray-bg'>" + item.CompanyName + "</td><td>" + item.CompanyType + "</td><td class='gray-bg'>" + item.CreationTime.ToString("dd MMMM yyyy") + "</td><td>" + item.ApprovalStatus + "</td>";
                                Input.string_var2 += table;
                                count++;
                            }
                            if (string.IsNullOrWhiteSpace(Input.string_var2))
                                Input.string_var2 = table;

                            Input.bool_var0 = false;
                            Input.bool_var2 = false;
                            return View(Input);
                    }
                    exComp = _context.Company_Registration.Include(x => x.User).Where(x => x.CompanyName.Equals(Input.string_var0) && x.CompanyType.Equals(Input.string_var10) && x.IsDeleted == false && x.RegCompleted == false && x.IsCacAvailable == true).FirstOrDefault();
                    if (exComp != null)
                    {
                        var simCopanies1 = _context.Company_Registration.Where(x => x.CompanyName.Contains(Input.string_var0) && x.RegCompleted == true).ToList();
                        foreach (var item in simCopanies1)
                        {
                            Input.list_var0.Add(item.CompanyName);
                            table = "<td>" + count + "</td><td class='gray-bg'>" + item.CompanyName + "</td><td>" + item.CompanyType + "</td><td class='gray-bg'>" + item.CreationTime.ToString("dd MMMM yyyy") + "</td><td>" + item.ApprovalStatus + "</td>";
                            Input.string_var2 += table;
                            count++;
                        }
                        if (string.IsNullOrWhiteSpace(Input.string_var2))
                            Input.string_var2 = table;
                        Input.bool_var0 = true;
                        Input.bool_var2 = false;
                        _temporaryVariables.string_var0 = exComp.Id.ToString();
                        HttpContext.Session.SetObject("TemporaryVariables", _temporaryVariables);
                        return View(Input);
                    }

                    count = 1;
                    var simCopanies = _context.Company_Registration.Where(x => x.CompanyName.Contains(Input.string_var0) && x.RegCompleted == true).ToList();
                    foreach (var item in simCopanies)
                    {
                        Input.list_var0.Add(item.CompanyName);
                        table = "<td>" + count + "</td><td class='gray-bg'>" + item.CompanyName + "</td><td>" + item.CompanyType + "</td><td class='gray-bg'>" + item.CreationTime.ToString("dd MMMM yyyy") + "</td><td>" + item.ApprovalStatus + "</td>";
                        Input.string_var2 += table;
                        count++;
                    }
                    if (string.IsNullOrWhiteSpace(Input.string_var2))
                        Input.string_var2 = table;
                    Input.bool_var0 = false;
                    Input.bool_var1 = false;
                    Input.bool_var2 = true;
                    return View(Input);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<bool> save_company(string companyName, string Type)
        {
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                var exComp = _context.Company_Registration.Where(x => x.CompanyName.ToLower() ==companyName.ToLower()).FirstOrDefault();
                if (exComp != null)
                {
                    _temporaryVariables.string_var0 = exComp.Id.ToString();
                    HttpContext.Session.SetObject("TemporaryVariables", _temporaryVariables);
                    return true;
                }
                else
                {
                    var regComp = new Company_Registration
                    {
                        CompanyName = companyName,
                        CompanyType = Type,
                        IsDeleted = false,
                        User = await _userService.get_User(_globalVariables.userid),
                        ApprovalStatus = "Awaiting Confirmation",
                        IsCacAvailable =false,
                        CreationTime = DateTime.Now
                    };
                    try
                    {
                        await _context.AddAsync(regComp);
                        await _context.SaveChangesAsync();
                        exComp = _context.Company_Registration.Where(x => x.CompanyName.Equals(companyName)).FirstOrDefault();
                        if (exComp != null)
                        {
                            _temporaryVariables.string_var0 = exComp.Id.ToString();
                            HttpContext.Session.SetObject("TemporaryVariables", _temporaryVariables);
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public ActionResult packages()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> packages(TemporaryVariables Input)
        {
            
            var company = _context.Company_Registration.Find(Guid.Parse(_temporaryVariables.string_var0));
            if (company != null)    
            {
                company.Package = GetProductId(Input.string_var0);
                company.TotalAmount = company.Package.Price;
                }
            try
            {
                _context.Update(company);
                await _context.SaveChangesAsync();
                return RedirectToAction("order_details");
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public Package GetProductId(string product)
        {
            var productname = "";
            if (product.Equals("1")) productname = "essential";
            else if (product.Equals("2")) productname = "standard";
            else productname = "premium";
            var company = _context.Package.Where(x => x.PackageName.ToLower().Contains(productname)).FirstOrDefault();
            return company;
        }
        public ActionResult order_summary()
        {
            return View();
        }
        public ActionResult order_details()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> order_details(TemporaryVariables Input)
        {
            var company = await _context.Company_Registration.Include(s=>s.Package).Where(x=>x.Id==Guid.Parse(_temporaryVariables.string_var0)).FirstOrDefaultAsync();
            if (company != null)
            {
                company.AlternateCompanyName = Input.string_var1;
                company.AlternateCompanyType = Input.string_var2;
                company.FinancialYearEnd = Input.string_var3;
                company.BusinessActivity = Input.string_var4;
                company.SndBusinessActivity = Input.string_var5;
                company.Address1 = Input.string_var6;
                company.Address2 = Input.string_var7;
                company.Postcode = Input.string_var8;
                company.IsAddressRegistered = Input.bool_var1;
                
            }
            try
            {
                _context.Update(company);
                await _context.SaveChangesAsync();
                return RedirectToAction("owner_details");
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public async Task<ActionResult> owner_details()
        {
            var temp = new TemporaryVariables();
            temp.string_array_temp0 = new string[30];
            for (int i = 0;i<temp.string_array_temp0.Length;i++)
            {
                temp.string_array_temp0[i] = "";
            }
            var company = await _context.Company_Registration.Include(s => s.Package).Where(x => x.Id == Guid.Parse(_temporaryVariables.string_var0)).FirstOrDefaultAsync();
            if (company != null)
            {
                temp.string_var0 = company.Package.PackageName;
                temp.string_var1 = "NGN" + company.Package.Price.ToString("#,##0.00");
                temp.string_var2 = "NGN" + company.Package.Price.ToString("#,##0.00");
                temp.string_var3 = "NGN" + ("2,400");
                temp.decimal_var0 = company.Package.Price;
                temp.decimal_var1 = 2400 + company.Package.Price;

            }
            await select_query();
            return View(temp);
        }

        public async Task<string> select_query()
        {
            var country = await _context.Settings.Where(x => x.code.ToLower() == "country").ToListAsync();
            ViewBag.country = new SelectList(country, "description", "description");
            return "";
        }
        [HttpPost]
        public async Task<ActionResult> owner_details(TemporaryVariables Input)
        {

            if (!await ValidateFilesFormat(Input.File1) || !await ValidateFilesFormat(Input.File2)|| !await ValidateFilesFormat(Input.File3)||
                !await ValidateFilesFormat(Input.File4)|| !await ValidateFilesFormat(Input.File5)|| !await ValidateFilesFormat(Input.File6))
            {
                ViewBag.message = "Invalid File Format Uploaded, Kindly upload image file or pdf";
                await select_query();
                return View(Input);
            }

            if (!ValidateFileSize(Input.File1) || !ValidateFileSize(Input.File2) || !ValidateFileSize(Input.File3) ||
                !ValidateFileSize(Input.File4) || !ValidateFileSize(Input.File5) || !ValidateFileSize(Input.File6))
            {
                ViewBag.message = "File Size Exceeded, Kindly upload image with less than 10mb size";
                await select_query();
                return View(Input);
            }
            var company = await _context.Company_Registration.Include(s=>s.company_Officers).Where(x=>x.Id==Guid.Parse(_temporaryVariables.string_var0)).FirstOrDefaultAsync();
            try
            {
                if (company != null)
                {
                    var passNumber = string.IsNullOrWhiteSpace(Input.string_array_temp0[7]) ? "" : Input.string_array_temp0[7];
                    var email = string.IsNullOrWhiteSpace(Input.string_array_temp0[11]) ? "" : Input.string_array_temp0[11];
                    var phone = string.IsNullOrWhiteSpace(Input.string_array_temp0[12]) ? "" : Input.string_array_temp0[12];
                    if (Input.bool_var0)
                        company.TotalAmount = Input.decimal_var0;
                    var getOfficers = _context.Company_Officers.Include(s=>s.Registration).Where(x => (x.Id_Number == passNumber && x.Registration.Id == company.Id) || (x.Email == email && x.Registration.Id == company.Id) || (x.Phone_No == phone && x.Registration.Id == company.Id)).FirstOrDefault();

                    if (getOfficers == null)
                    {
                        var officers = new Company_Officers
                        {
                            FullName = Input.string_array_temp0[0],
                            Gender = Input.string_array_temp0[1],
                            Id_Type = Input.string_array_temp0[6],
                            Id_Number = Input.string_array_temp0[7],
                            Nationality = Input.string_array_temp0[8],
                            Birth_Country = Input.string_array_temp0[9],
                            Dob = Input.string_array_temp0[10],
                            Email = Input.string_array_temp0[11],
                            Phone_No = Input.string_array_temp0[12],
                            Address1 = Input.string_array_temp0[13],
                            Address2 = Input.string_array_temp0[14],
                            PostalCode = Input.string_array_temp0[15],
                            MobileNo = Input.string_array_temp0[16],
                            Registration = company, 
                            Identification = await ConvertFileToByte(Input.File1),
                            CerficationOfBirth = await ConvertFileToByte(Input.File2),
                            Proficiency = await ConvertFileToByte(Input.File3),

                        };
                        company.company_Officers.Add(officers);
                        _context.Update(company);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("company_share");
                    }
                    else
                    {
                        return RedirectToAction("company_share");
                    }
                };
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public async Task<bool> ValidateFilesFormat(IFormFile file)
        {
            if (file == null)
                return true;
            return ImageWriterHelper.GetImageFormat(await ConvertFileToByte(file)) != ImageWriterHelper.ImageFormat.unknown;
            if (ImageWriterHelper.GetPdfFormat(await ConvertFileToByte(file)))
                return true;
        }

        public bool ValidateFileSize(IFormFile file)
        {
            if (file == null)
                return true;
            if (file.Length< 10000000)
                return true;

            return false;
        }

        public async Task<byte[]> ConvertFileToByte(IFormFile file)
        {
            if (file == null)
                return null;
                using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
            }
        }

        public async Task<ActionResult> company_share()
        {
            var temp = new TemporaryVariables();
            var company = await _context.Company_Registration.Include(s => s.Package).Include(u=>u.User).Where(x => x.Id == Guid.Parse(_temporaryVariables.string_var0)).FirstOrDefaultAsync();
            if (company != null)
            {
                temp.string_var0 = company.Package.PackageName;
                temp.string_var1 = "NGN" + company.Package.Price.ToString("#,##0.00");
                temp.string_var2 = "NGN" + company.Package.Price.ToString("#,##0.00");
                temp.string_var3 = "NGN" + ("2,400");
                temp.string_var4 = company.CompanyName + " " + company.CompanyType;
                temp.string_var5 = company.User.FirstName + " " + company.User.LastName;

            }
            return View(temp);
        }

        [HttpPost]
        public async Task<ActionResult> company_share(TemporaryVariables Input)
        {
            var company =await _context.Company_Registration.Include(u => u.User).Where(s=>s.Id == Guid.Parse(_temporaryVariables.string_var0)).FirstOrDefaultAsync();
            if (company != null)
            {
                company.CompanyCapitalCurrency = Input.string_var6;
                company.NoOfSharesIssue = Input.int_var0;
                company.SharePrice = Input.decimal_var0;
                company.SharesAllocated = Input.decimal_var1;
                company.ShareHolderName = company.User.FirstName + " " + company.User.LastName;
            }
            try
            {
                _context.Update(company);
                await _context.SaveChangesAsync();
                return RedirectToAction("order_review");
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        [HttpPost]
        public async Task<bool> Verify_PayStack(string reference)
        {
            var testOrLiveSecret = _appSettings.PayStackSecret;
            var api = new PayStackApi(testOrLiveSecret);
            // Verifying a transaction
            var verifyResponse = api.Transactions.Verify(reference); // auto or supplied when initializing;
            if (verifyResponse.Status)
            {
                var company = await _context.Company_Registration.Include(a=>a.Payments).Include(x=>x.User).Include(s=>s.Package).Where(x=>x.Id==Guid.Parse(_temporaryVariables.string_var0)).FirstOrDefaultAsync();
                company.RegCompleted = true;
                company.ModificationTime = DateTime.Now;
                company.ApprovalStatus = "Awaiting Approval";
                var req = new
                {
                    email = company.User.Email,
                    amount = company.Package.Price,
                    currency = string.IsNullOrWhiteSpace(company.CompanyCapitalCurrency)?"": company.CompanyCapitalCurrency,
                    phone_no = company.User.PhoneNumber,
                };

                if (company.Payments.Any())
                {
                    foreach (var item in company.Payments)
                    {
                        if (!item.Status)
                        {
                            var payment = new Payments
                            {
                                ApiRequest = JsonConvert.SerializeObject(req),
                                ApiResponse = JsonConvert.SerializeObject(verifyResponse),
                                Status = verifyResponse.Status,
                                Message = verifyResponse.Message,
                                Amount = verifyResponse.Data.Amount,
                                Total = verifyResponse.Data.Amount,
                                Registration = company,
                                PaymentType = "Online Payment"
                                                            };
                            await _context.AddAsync(payment);
                        }
                    }
                }
                else
                {
                    var payment = new Payments
                    {
                        ApiRequest = JsonConvert.SerializeObject(req),
                        ApiResponse = JsonConvert.SerializeObject(verifyResponse),
                        Status = verifyResponse.Status,
                        Message = verifyResponse.Message,
                        Amount = verifyResponse.Data.Amount,
                        Total = verifyResponse.Data.Amount,
                        Registration = company,
                        PaymentType = "Online Payment"
                    };
                    await _context.AddAsync(payment);
                };
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
        public ActionResult order_review()
        {
            var companyInfo = new TemporaryVariables();
            var company = _context.Company_Registration.Include(c=>c.User).Include(x => x.Package).Include(s => s.addOnServices).Include(o => o.company_Officers).Where(x => x.Id == Guid.Parse(_temporaryVariables.string_var0)).FirstOrDefault();
            if (company != null)
            {
                companyInfo = new TemporaryVariables
                {
                    string_var0 = company.CompanyName + " " +company.CompanyType,
                    string_var1 = company.AlternateCompanyName + " " + company.AlternateCompanyType,
                    string_var2 = company.BusinessActivity + " and " + company.SndBusinessActivity,
                    string_var3 = company.FinancialYearEnd,
                    string_var4 = company.Address1,
                    string_var5 = company.Address2,
                    string_var6 = company.CompanyCapitalCurrency,
                    int_var0 =    company.NoOfSharesIssue,
                    string_var7 = company.SharePrice.ToString(),
                    string_var8 = company.ShareHolderName,
                    decimal_var0 = company.SharesAllocated,
                    string_var9 = company.CreationTime.ToString(),
                    string_var10 = company.Package.PackageName,
                    string_var11 = company.Package.CreationTime.ToString(),
                    string_var12 = company.Package.Price.ToString(),
                    string_var15 = company.User.Email,
                    string_var16 = company.User.PhoneNumber,
                    string_var17 = "NGN"+ company.TotalAmount.ToString("#,##0.00"),
                    decimal_var1 = company.TotalAmount,
                };
                if (company.company_Officers != null && company.company_Officers.Any())
                {
                    foreach (var item in company.company_Officers)
                    {
                        string table = "<table class='hover'><tbody>";
                        table += "<tr><th colspan='2' class='text-left'> OFFICER 1 - " + item.FullName + " </th></tr>";
                        table += "<tr><td width='200'>Full Name</td><td class='text-bold'>" + item.FullName + "</td></tr>";
                        table += "<tr><td>Gender</td><td colspan='2' class='text-bold'>" + item.Gender + "</td></tr>";
                        table += "<tr><td>Position</td><td class='text-bold'>" + item.Designation + "</td></tr>";
                        table += "<tr><td>ID Number</td><td class='text-bold'> " + item.Id_Number + " </td></tr>";
                        table += "<tr><td>ID Type</td><td class='text-bold'>" + item.Id_Type + "</td></tr>";
                        table += "<tr><td>Date of Birth</td><td class='text-bold'>" + item.Dob + "</td></tr>";
                        table += "<tr><td>Country of Birth</td><td class='text-bold'>" + item.Birth_Country + "</td></tr>";
                        table += "<tr><td>Nationality</td><td class='text-bold'>" + item.Nationality + "</td></tr>";
                        table += "<tr><td>Address Line 1</td><td class='text-bold'>" + item.Address1 + "</td></tr>";
                        table += "<tr><td>Address Line 2</td><td class='text-bold'>" + item.Address2 + "</td></tr>";
                        table += "<tr><td>Postcode</td><td class='text-bold'>" + item.PostalCode + "</td></tr>";
                        table += "<tr><td>Mobile Phone</td><td class='text-bold'>" + item.MobileNo + "</td></tr>";
                        table += "<tr><td>Work Phone</td><td class='text-bold'>" + item.Phone_No + "</td></tr>";
                        table += "<tr><td>Email</td><td class='text-bold'>" + item.Email + "</td></tr></tbody></table>";
                        companyInfo.string_var13 += table;
                    }
                }
                if (company.addOnServices != null && company.addOnServices.Any())
                {
                    foreach (var item in company.addOnServices)
                    {
                        string officers = "<tr><td>"+item.ServiceName+"</td>";
                        officers += "<td class='text-center'>"+item.CreationTime+"</td>";
                        officers += "<td class='text-right'>" + item.Price + "</td></tr>";
                        companyInfo.string_var14 += officers;
                    }
                }
            }
            return View(companyInfo);
        }
        public ActionResult attendance()
        {
            return View();
        }
        public ActionResult cashless_payments()
        {
            return View();
        }
        public async Task<ActionResult> chat(int Id)
        {
            int count = 0;
            var user = await _userService.get_User_By_Session();
            var temp = new TemporaryVariables
            {
                ChatModel = new ChatModel
                {
                    ViewChatList = new List<ChatModel.ChatList>(),
                    ViewChatDetails = new List<ChatModel.ChatDetails>(),
                }
            };
            temp.string_var0 = user.FirstName + " " + user.LastName;
            var chats = await _context.ChatHeader.Include(x => x.ChatThread).Where(s => s.IsDeleted == false && s.User == user).OrderByDescending(m=>m.CreationTime).ToListAsync();
            if (user.Role.ToLower().Equals("admin"))
            chats = await _context.ChatHeader.Include(x => x.ChatThread).Where(s => s.IsDeleted == false).OrderByDescending(m => m.CreationTime).ToListAsync();
            foreach (var item in chats)
            {
                count = 0;
                foreach (var x in item.ChatThread)
                {
                //    temp.ChatModel.ViewChatDetails.Add(new ChatModel.ChatDetails
                //    {
                //        Message1 = x.Body,
                //    });
                    if (!x.IsRead)
                        count++;
                }
                temp.ChatModel.ViewChatList.Add(new ChatModel.ChatList
                {
                    Date = item.CreationTime,
                    Status = item.Group,
                    TicketNumber = item.Id,
                    NoOfNew = count
                });
                
                
            };
                if (Id != 0)
                {

                    var getChatById = chats.Where(x => x.Id == Id).FirstOrDefault();
                    temp.int_var0 = getChatById.Id;
                    foreach (var x in await _context.ChatThread.Include(s => s.User).Where(x => x.IsDeleted == false && x.Chat == getChatById).ToListAsync())
                    {
                        temp.ChatModel.ViewChatDetails.Add(new ChatModel.ChatDetails
                        {
                            Message1 = x.Body,
                            User = x.User.Role,
                        });
                        if (x.User == user)
                        {
                            x.IsRead = true;
                            _context.Update(x);
                        }
                    }
                temp.string_var0 = getChatById.User.FirstName + " " + getChatById.User.LastName;
                await _context.SaveChangesAsync();
                }

                
            return View(temp);
        }

        [HttpPost]
        public async Task<ActionResult> chat(TemporaryVariables Input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.get_User_By_Session();
                var chat =await  _context.ChatHeader.FindAsync(Input.int_var0);
                var ChatThread = new List<ChatThread>()
                    { new ChatThread
                    {
                        User = user,
                        Body = Input.string_var2,
                        IsRead = false,
                        CreationTime = DateTime.Now,
                        CreatorUserId = user.Id,
                        Chat = chat,
                    }
                    };
                await _context.AddRangeAsync(ChatThread);
                await _context.SaveChangesAsync();
                Input.string_var2 = "";
            }
            return RedirectToAction("chat", new { Id = Input.int_var0 });
        }

         public async Task<ActionResult> new_ticket()
        {
            var user = await _userService.get_User_By_Session();
            var Input = new TemporaryVariables
            {
                string_var0 = user.FirstName + " " + user.LastName,
                string_var1 = user.Email
            };
            var companies = from bg in await _companyService.GetCompanies()
                            select new { c1 = bg.Id.ToString(), c2 = bg.CompanyName + " " + bg.CompanyType };
            ViewBag.companies = new SelectList(companies, "c1","c2");
            return View(Input);
        }

        [HttpPost]
        public async Task<ActionResult> new_ticket(TemporaryVariables Input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.get_User_By_Session();
                var cHeader = new ChatHeader
                {
                    User = user,
                    Company = string.IsNullOrWhiteSpace(Input.string_var3)?null: await _companyService.GetCompanyById(Guid.Parse(Input.string_var3)),
                    CreationTime = DateTime.Now,
                    CreatorUserId = user.Id,
                    Subject = Input.string_var2,
                    Body = Input.string_var4,
                    Group="New",
                    ChatThread = new List<ChatThread>()
                    { new ChatThread
                    {
                        User = user,
                        Body = Input.string_var4,
                        IsRead = false,
                        CreationTime = DateTime.Now,
                        CreatorUserId = user.Id
                    }
                    }
                };
                await _context.AddAsync(cHeader);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("chat");
        }

        public ActionResult GetConfigurationValue(string sectionName, string paramName)
        {
            var parameterValue = _configuration[$"{sectionName}:{paramName}"];
            return Json(new { parameter = parameterValue });
        }
        public ActionResult departments()
        {
            return View();
        }
        public ActionResult doctor_edit()
        {
            return View();
        }
        public ActionResult doctor_profile()
        {
            return View();
        }
        public ActionResult edit_member()
        {
            return View();
        }
        public ActionResult events()
        {
            return View();
        }
        public ActionResult expenses()
        {
            return View();
        }
        public ActionResult holidays()
        {
            return View();
        }
        public ActionResult insurance_company()
        {
            return View();
        }
        public ActionResult leaves()
        {
            return View();
        }
        public ActionResult member_profile()
        {
            return View();
        }
        public ActionResult patient_edit()
        {
            return View();
        }
        public ActionResult patient_profile()
        {
            return View();
        }
        public ActionResult payment_invoice()
        {
            return View();
        }
        public ActionResult salary()
        {
            return View();
        }

    }
}