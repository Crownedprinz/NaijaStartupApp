using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NaijaStartupApp.Controllers
{
    public class OthersController : Controller
    {

        public ActionResult email_inbox()
        {
            return View();
        }


    }
}