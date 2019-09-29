using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NaijaStartupApp.Controllers
{
    public class DashboardController : Controller
    {
        
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

    }
}