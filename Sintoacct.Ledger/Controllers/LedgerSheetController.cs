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
    }
}