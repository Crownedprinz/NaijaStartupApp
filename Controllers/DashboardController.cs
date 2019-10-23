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

namespace NaijaStartupApp.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        private AppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;
        GlobalVariables _globalVariables;
        TemporaryVariables _temporaryVariables;
        public DashboardController(ApplicationDbContext context,
            IOptions<AppSettings> appsettings,
            IUserService userService,IHttpContextAccessor hcontext,
            ICompanyService companyService)
        {
            _context = context;
            _appSettings = appsettings.Value;
            _userService = userService;
            _companyService = companyService;
            _globalVariables = hcontext.HttpContext.Session.GetObject<GlobalVariables>("GlobalVariables");
            _temporaryVariables = hcontext.HttpContext.Session.GetObject<TemporaryVariables>("TemporaryVariables");
        }

        public async Task<ActionResult> Index()
        {
            var temp = new TemporaryVariables
            {
                int_var0 = _companyService.Company_Count()
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

            return View(_context.Company_Registration.Where((x => x.IsDeleted == false)).OrderByDescending(s => s.CreationTime)
                .Select(x => new TemporaryVariables
                {
                    string_var0 = x.CompanyName,
                    date_var0 = x.CreationTime,
                    string_var1 = x.FinancialYearEnd,
                    string_var2 = x.Address1,
                    string_var3 = x.Id.ToString(),
                    string_var4 = x.ApprovalStatus,
                    string_var10 = x.CacRegistrationNumber,
                    bool_var0 = x.RegCompleted,
                }).ToList());
        }
        [HttpGet]
        public ActionResult edit_companies()
        {
            return View();
        }
        [HttpPut]
        public async Task<bool> edit_companies(string Id)
        {
            var company = await _context.Company_Registration.FindAsync(Id);
            if (company != null)
            {
                return true;
            }
            else
                return false;
            return false;
        }
        [HttpDelete]
        public async Task<bool> delete_companies(string Id)
        {
            var company = await _context.Company_Registration.FindAsync(Id);
            if (company != null)
            {
                company.IsDeleted = true;
                company.ModificationTime = DateTime.Now;
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
            return View(_context.Payments.Include(x=>x.Registration).Where((x => x.IsDeleted == false)).OrderByDescending(s => s.CreationTime)
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
            return View(_context.Company_Registration.Include(x => x.Package).Include(s=>s.User).Where((x => x.IsDeleted == false)).OrderByDescending(s => s.CreationTime)
                .Select(x => new TemporaryVariables
                {
                    string_var0 = x.User.FirstName +" "+x.User.LastName,
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

        [HttpPost]
        public async Task<ActionResult> approve_company(string Id)
        {
            var company = _context.Company_Registration.Where(x => x.IsDeleted == false && x.Id.ToString() == Id).FirstOrDefault();
            if (company != null)
            {
                company.ApprovalStatus = "Confirmed";
                company.ModificationTime = DateTime.Now;
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
            var company = await _context.Company_Registration.Include(x => x.Package).Include(s => s.addOnServices).Include(o => o.company_Officers).Where(x => x.Id.ToString() == Id).FirstOrDefaultAsync();
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
                    string_var12 = company.Package.Price.ToString(),
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
                        table += "<tr><td>Proof of ID</td><td class='text-bold'><ul></ul></td></tr>";
                        table += "<tr><td>Proof of Address</td><td class='text-bold'><ul></ul></td></tr></tbody></table>";
                        companyInfo.string_var13 += table;
                    }
                }
                if (company.addOnServices != null && company.addOnServices.Any())
                {
                    foreach (var item in company.addOnServices)
                    {
                        string officers = "<tr><td>" + item.ServiceName + "</td>";
                        officers += "<td class='text-center'>" + item.CreationTime + "</td>";
                        officers += "<td class='text-right'>" + item.Price + "</td></tr>";
                        companyInfo.string_var14 += officers;
                    }
                }
            }
            return View(companyInfo);
        }

        public ActionResult all_rooms()
        {
            return View();
        }
        public ActionResult all_staff()
        {
            return View();
        }
        public ActionResult new_company()
        {
            TemporaryVariables tempValues = new TemporaryVariables
            {
                bool_var0 = false,
                list_var0 = new List<string>()
            };
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
                var exComp = _context.Company_Registration.Where(x => x.CompanyName.Equals(Input.string_var0) && x.IsDeleted == false && x.RegCompleted == true).Select(e => e.CompanyName).FirstOrDefault();
                if (exComp != null)
                {
                    var simCopanies = _context.Company_Registration.Where(x => x.CompanyName.Contains(Input.string_var0) && x.RegCompleted == true).ToList();

                    Input.bool_var0 = false;
                    Input.bool_var1 = true;
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
                    Input.bool_var0 = true;
                    Input.bool_var1 = false;
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
                        ApprovalStatus = "Processing",
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
            var company = _context.Company_Registration.Find(Guid.Parse(_temporaryVariables.string_var0));
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
        public ActionResult owner_details()
        {
            var temp = new TemporaryVariables();
            temp.string_array_temp0 = new string[30];
            for (int i = 0;i<temp.string_array_temp0.Length;i++)
            {
                temp.string_array_temp0[i] = "";
            }

            return View(temp);
        }
        [HttpPost]
        public async Task<ActionResult> owner_details(TemporaryVariables Input)
        {
            var company = _context.Company_Registration.Find(Guid.Parse(_temporaryVariables.string_var0));
            try
            {
                if (company != null)
                {
                    var passNumber = string.IsNullOrWhiteSpace(Input.string_var3) ? "" : Input.string_var3;
                    var email = string.IsNullOrWhiteSpace(Input.string_var9) ? "" : Input.string_var9;
                    var phone = string.IsNullOrWhiteSpace(Input.string_var10) ? "" : Input.string_var10;

                    var getOfficers = _context.Company_Officers.Where(x => (x.Id_Number == passNumber) || (x.Email == email) || (x.Phone_No == phone)).FirstOrDefault();

                    if (getOfficers == null)
                    {
                        var officers = new Company_Officers
                        {
                            FullName = Input.string_array_temp0[0],
                            Gender = Input.string_array_temp0[1],
                            Id_Type = Input.string_array_temp0[2],
                            Id_Number = Input.string_array_temp0[3],
                            Nationality = Input.string_array_temp0[4],
                            Birth_Country = Input.string_array_temp0[5],
                            Dob = Input.string_array_temp0[6],
                            Email = Input.string_array_temp0[7],
                            Phone_No = Input.string_array_temp0[8],
                            Address1 = Input.string_array_temp0[9],
                            Address2 = Input.string_array_temp0[10],
                            PostalCode = Input.string_array_temp0[11],
                            MobileNo = Input.string_array_temp0[12],
                            Registration = company,
                        };

                        await _context.AddAsync(officers);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("order_review");
                    }
                    else
                    {
                        //officer already exist;
                    }
                };
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult company_share()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> company_share(TemporaryVariables Input)
        {
            var company = _context.Company_Registration.Find(Guid.Parse(_temporaryVariables.string_var0));
            if (company != null)
            {
                company.CompanyCapitalCurrency = Input.string_var0;
                company.NoOfSharesIssue = Input.int_var0;
                company.SharePrice = Input.decimal_var0;
                company.SharesAllocated = Input.decimal_var1;
                company.ShareHolderName = Input.string_var1;
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
            var company = _context.Company_Registration.Include(x => x.Package).Include(s => s.addOnServices).Include(o => o.company_Officers).Where(x => x.Id == Guid.Parse(_temporaryVariables.string_var0)).FirstOrDefault();
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
                    string_var12 = company.Package.Price.ToString(),
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
                        table += "<tr><td>Proof of ID</td><td class='text-bold'><ul></ul></td></tr>";
                        table += "<tr><td>Proof of Address</td><td class='text-bold'><ul></ul></td></tr></tbody></table>";
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
        public ActionResult chat()
        {
            return View();
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