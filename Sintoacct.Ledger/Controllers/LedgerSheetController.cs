using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;
using Newtonsoft.Json;

namespace Sintoacct.Ledger.Controllers
{
    public class LedgerSheetController : BaseController
    {
        private readonly IAccountHelper _account;

        public LedgerSheetController(IAccountHelper account)
        {
            _account = account;
        }

        /// <summary>
        /// 明细账
        /// </summary>
        /// <returns></returns>
        [ClaimsAuthorize("role", "accountant")]
        public ActionResult DetailAccount()
        {
            return View();
        }

        /// <summary>
        /// 总账
        /// </summary>
        /// <returns></returns>
        [ClaimsAuthorize("role", "accountant")]
        public ActionResult GeneralLedger()
        {
            return View();
        }

        /// <summary>
        /// 科目余额表
        /// </summary>
        /// <returns></returns>
        [ClaimsAuthorize("role", "accountant")]
        public ActionResult AccountBalance()
        {
            return View();
        }


        /// <summary>
        /// 凭证汇总表
        /// </summary>
        /// <returns></returns>
        [ClaimsAuthorize("role", "accountant")]
        public ActionResult VoucherSummary()
        {
            return View();
        }

        /// <summary>
        /// 多栏账
        /// </summary>
        /// <returns></returns>
        [ClaimsAuthorize("role", "accountant")]
        public ActionResult MultiColumn()
        {
            MultiColumnInitViewModels mcvm = new MultiColumnInitViewModels();
            mcvm.AccountsJson = JsonConvert.SerializeObject(_account.GetAccountTree().children);
            return View(mcvm);
        }
    }
}