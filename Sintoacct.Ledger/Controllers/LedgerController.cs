using System.Web.Mvc;
using Sintoacct.Ledger.Services;
using System;
using System.Web;
using System.Security.Claims;
using Sintoacct.Ledger.Models;
using Microsoft.AspNet.Identity;

namespace Sintoacct.Ledger.Controllers
{
    public class LedgerController : Controller
    {
        private ClaimsIdentity _identity;
        private ICacheHelper _cache;

        public LedgerController(HttpContextBase context,ICacheHelper cache)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _cache = cache;
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
                UserCacheModel userCache = new UserCacheModel();
                userCache.AccountBookID = abidStr;
                _cache.SetUserCache(userCache);
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