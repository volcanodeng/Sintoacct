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

        [ClaimsAuthorize("role", "business")]
        public ActionResult TopFrame()
        {
            return View();
        }

        [ClaimsAuthorize("role", "business")]
        public ActionResult BizProgress()
        {
            return View();
        }

        [ClaimsAuthorize("role", "business")]
        public JsonResult GetBizPersons()
        {
            return Json(_company.GetBizPersons());
        }
        
    }
}