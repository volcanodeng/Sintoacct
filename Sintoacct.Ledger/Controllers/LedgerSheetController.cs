using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Controllers
{
    public class LedgerSheetController : Controller
    {
        public LedgerSheetController()
        {

        }

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult DetailAccount()
        {
            return View();
        }
    }
}