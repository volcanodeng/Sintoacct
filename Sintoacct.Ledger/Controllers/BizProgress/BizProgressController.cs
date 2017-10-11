using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace Sintoacct.Ledger.Controllers
{
    public class BizProgressController : BaseController
    {
        private readonly ClaimsIdentity _identity;

        public BizProgressController(HttpContextBase context)
        {
            _identity = context.User.Identity as ClaimsIdentity;
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

        
    }
}