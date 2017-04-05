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
        private readonly IAccountHelper _account;

        public LedgerController(HttpContextBase context,
                                ICacheHelper cache,
                                IAuxiliaryHelper auxiliary,
                                IAccountHelper account)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _cache = cache;
            _auxiliary = auxiliary;
            _account = account;
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
                userCache.AccountBookID = abid;
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

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult Account()
        {
            List<AccountCategory> accCates = _account.GetMainAccountCategory();
            List<AuxiliaryType> auxTypes = _auxiliary.GetAuxiliaryType();

            AccountControllerViewModel account = new AccountControllerViewModel();
            account.AccountCategorys = Mapper.Map<List<AccountCategoryViewModel>>(accCates);
            account.AuxiliaryTypes = Mapper.Map<List<AuxiliaryTypeViewModel>>(auxTypes);
            return View(account);
        }

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult InitialBalance()
        {
            List<AccountCategory> accCates = _account.GetMainAccountCategory();
            AccountControllerViewModel account = new AccountControllerViewModel();
            account.AccountCategorys = Mapper.Map<List<AccountCategoryViewModel>>(accCates);
            account.AccountCategoriesWithQuantity = _account.GetAccountCategoriesWithQuantity();

            account.AuxCustom = Mapper.Map<List<AuxiliaryViewModel>>(_auxiliary.GetAuxiliaryOfType(1));
            account.AuxSuppliers = Mapper.Map<List<AuxiliaryViewModel>>(_auxiliary.GetAuxiliaryOfType(2));
            account.AuxEmployee = Mapper.Map<List<AuxiliaryViewModel>>(_auxiliary.GetAuxiliaryOfType(3));
            account.AuxProject = Mapper.Map<List<AuxiliaryViewModel>>(_auxiliary.GetAuxiliaryOfType(4));
            account.AuxSector = Mapper.Map<List<AuxiliaryViewModel>>(_auxiliary.GetAuxiliaryOfType(5));
            account.AuxInventory = Mapper.Map<List<AuxiliaryViewModel>>(_auxiliary.GetAuxiliaryOfType(6));
            return View(account);
        }

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult Voucher()
        {
            return View();
        }

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult ViewVoucher()
        {
            return View();
        }
    }
}