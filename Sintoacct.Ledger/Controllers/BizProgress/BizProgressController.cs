using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Sintoacct.Ledger.BizProgressServices;
using Sintoacct.Ledger.Services;

namespace Sintoacct.Ledger.Controllers
{
    public class BizProgressController : BaseController
    {
        private readonly ClaimsIdentity _identity;
        private readonly IBizProgressService _progress;
        private readonly ICompanyHelper _company;

        public BizProgressController(HttpContextBase context,IBizProgressService progress,ICompanyHelper company)
        {
            _identity = context.User.Identity as ClaimsIdentity;

            _progress = progress;

            _company = company;
        }

        public ActionResult TopFrame()
        {
            return View();
        }

        public ActionResult BizProgress()
        {
            return View();
        }


        public ActionResult Category()
        {
            return View();
        }

        public ActionResult BizItem()
        {
            return View();
        }

        public ActionResult BizSteps()
        {
            return View();
        }

        public JsonResult GetBizPersons()
        {
            return Json(_company.GetBizPersons());
        }
        
    }
}