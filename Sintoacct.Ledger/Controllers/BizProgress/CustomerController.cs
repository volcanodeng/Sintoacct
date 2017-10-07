using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace Sintoacct.Ledger.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ClaimsIdentity _identity;

        public CustomerController(HttpContextBase context)
        {
            _identity = context.User.Identity as ClaimsIdentity;
        }

        

        public ActionResult Customers()
        {
            return View();
        }

        public ActionResult CostSetting()
        {
            return View();
        }

        public ActionResult MarketingChain()
        {
            return View();
        }
    }
}