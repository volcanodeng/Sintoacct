using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Sintoacct.Ledger.Models;
using System.Security.Claims;

namespace Sintoacct.Ledger.Controllers
{
    public class LedgerController : Controller
    {
        public ActionResult Index()
        {
            ClaimsIdentity ci = User.Identity as ClaimsIdentity;

            LedgerViewModels ledger = new LedgerViewModels() {
                UserId = ci.GetUserId(),
                UserName = ci.GetUserName(),
                IsAuth = ci.IsAuthenticated
            };
            return View(ledger);
        }
    }
}