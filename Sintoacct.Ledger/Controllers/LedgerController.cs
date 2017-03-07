using System.Web.Mvc;
using Sintoacct.Ledger.Services;
using System;
using System.Security.Claims;

namespace Sintoacct.Ledger.Controllers
{
    public class LedgerController : Controller
    {

        public LedgerController()
        {
        }


        [ClaimsAuthorize("role", "accountant")]
        public ActionResult AccountBook()
        {
            return View();
        }

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult LedgerFrame()
        {
            string abidStr = Request.QueryString["abid"];
            Guid abid; 
            if(!string.IsNullOrEmpty(abidStr) && Guid.TryParse(abidStr,out abid))
            {
                var identity = this.HttpContext.User.Identity as ClaimsIdentity;
                identity.AddClaim(new Claim(Constants.ClaimAccountBookID, abidStr));
            }
            else
            {
                RedirectToAction("AccountBook");
            }

            return View();
        }

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult CertWord()
        {
            return View();
        }
    }
}