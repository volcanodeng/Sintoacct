using System.Web.Mvc;
using Sintoacct.Ledger.Services;
using System;
using System.Web;
using System.Security.Claims;
using Sintoacct.Ledger.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using AutoMapper;

namespace Sintoacct.Ledger.Controllers
{
    public class LedgerController : Controller
    {
        private readonly ClaimsIdentity _identity;
        private readonly ICacheHelper _cache;
        private readonly IAuxiliaryHelper _auxiliary;

        public LedgerController(HttpContextBase context,ICacheHelper cache,IAuxiliaryHelper auxiliary)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _cache = cache;
            _auxiliary = auxiliary;
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

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult AuxiliaryType()
        {
            List<AuxiliaryType> auxTypes = _auxiliary.GetAuxiliaryType();
            return View(auxTypes);
        }

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult Auxiliary(int id)
        {
            List<AuxiliaryType> auxTypes = _auxiliary.GetAuxiliaryType();
            return View(new AuxiliaryListViewModel() { AuxType = id, AuxTypes = Mapper.Map<List<AuxiliaryTypeViewModel>>(auxTypes) });
        }
    }
}