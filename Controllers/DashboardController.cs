using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NaijaStartupApp.Data;
using NaijaStartupApp.Helpers;
using PayStack.Net;
using static NaijaStartupApp.Models.NsuDtos;
using static NaijaStartupApp.Models.NsuVariables;

namespace NaijaStartupApp.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        private AppSettings _appSettings;
        public DashboardController(ApplicationDbContext context, IOptions<AppSettings> appsettings)
        {
            _context = context;
            _appSettings = appsettings.Value;
        }

        public ActionResult Index()
        {
            return View();
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

            return View(_context.Company_Registration.Where(x => x.IsDeleted != false).OrderByDescending(s => s.CreationTime)
                .Select(x => new TemporaryVariables
                {
                    int_var0 = count + 1,
                    string_var0 = x.CompanyName,
                    date_var0 = x.CreationTime,
                    string_var1 = x.FinancialYearEnd,
                    string_var2 = x.Address1,
                }).ToList());
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
            return View();
        }
        public ActionResult my_orders()
        {
            return View();
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

            return View(tempValues);
        }
        [HttpPost]
        public ActionResult new_company(TemporaryVariables Input)
        {
            if (ModelState.IsValid)
            {
                if (Input.list_var0 == null) Input.list_var0 = new List<string>();
                var exComp = _context.Company_Registration.Where(x => x.CompanyName.Equals(Input.string_var0)).Select(e => e.CompanyName).FirstOrDefault();
                if (exComp != null)
                {
                    var simCopanies = _context.Company_Registration.Where(x => x.CompanyName.Contains(Input.string_var0)).Select(e => e.CompanyName).ToList();

                    Input.bool_var0 = false;
                    foreach (var item in simCopanies)
                    {
                        Input.list_var0.Add(item);
                    }
                    return View(Input);
                }
                else
                {
                    var simCopanies = _context.Company_Registration.Where(x => x.CompanyName.Contains(Input.string_var0)).Select(e => e.CompanyName).ToList();
                    foreach (var item in simCopanies)
                    {
                        Input.list_var0.Add(item);
                    }
                    Input.bool_var0 = true;
                    return View(Input);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<bool> save_company(string companyName)
        {
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                var exComp = _context.Company_Registration.Where(x => x.CompanyName.Equals(companyName)).Select(e => e.CompanyName).FirstOrDefault();
                if (exComp != null)
                {
                    return true;
                }
                else
                {
                    var regComp = new Company_Registration
                    {
                        CompanyName = companyName,
                        IsDeleted = false
                    };
                    try
                    {
                        await _context.AddAsync(regComp);
                        await _context.SaveChangesAsync();
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
            var company = _context.Company_Registration.Find(Guid.Parse("523DC9AD-2834-4AB3-8C50-08D751CAE14C"));
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
            var company = _context.Company_Registration.Find(Guid.Parse("523DC9AD-2834-4AB3-8C50-08D751CAE14C"));
            if (company != null)
            {
                company.AlternateCompanyName = Input.string_var0;
                company.AlternateCompanyType = Input.string_var1;
                company.FinancialYearEnd = Input.string_var2;
                company.BusinessActivity = Input.string_var3;
                company.SndBusinessActivity = Input.string_var4;
                company.Address1 = Input.string_var5;
                company.Address2 = Input.string_var6;
                company.Postcode = Input.string_var7;
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
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> owner_details(TemporaryVariables Input)
        {
            var company = _context.Company_Registration.Find(Guid.Parse("523DC9AD-2834-4AB3-8C50-08D751CAE14C"));
            try
            {
                if (company != null)
                {
                    var officers = new Company_Officers
                    {
                        FullName = Input.string_var0,
                        Gender = Input.string_var1,
                        Id_Type = Input.string_var2,
                        Id_Number = Input.string_var3,
                        Nationality = Input.string_var4,
                        Birth_Country = Input.string_var5,
                        Dob = Input.string_var6,
                        Email = Input.string_var9,
                        Phone_No = Input.string_var10,
                        Address1 = Input.string_var11,
                        Address2 = Input.string_var12,
                        PostalCode = Input.string_var13,
                        MobileNo = Input.string_var14,
                        Registration = company,
                    };

                    await _context.AddAsync(officers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("owner_details");
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
            var company = _context.Company_Registration.Find(Guid.Parse("523DC9AD-2834-4AB3-8C50-08D751CAE14C"));
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
        public bool Verify_PayStack(string reference)
        {
            var testOrLiveSecret = _appSettings.PayStackSecret;
            var api = new PayStackApi(testOrLiveSecret);
            // Verifying a transaction
            var verifyResponse = api.Transactions.Verify(reference); // auto or supplied when initializing;
            if (verifyResponse.Status)
            {
                return true;
                /*
                    You can save the details from the json object returned above so that the authorization code
                    can be used for charging subsequent transactions

                    // var authCode = verifyResponse.Data.Authorization.AuthorizationCode
                    // Save 'authCode' for future charges!
                */
            }
            return false;
        }
        public ActionResult order_review()
        {
            return View();
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