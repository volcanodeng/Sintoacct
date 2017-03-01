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
        private readonly IAccountBookHelper _acctBook;

        public LedgerController(IAccountBookHelper acctBook)
        {
            _acctBook = acctBook;
        }

        [Authorize(Roles = "abc@qq.com")]
        public ActionResult Index()
        {
            return View(_acctBook.GetLedger());
        }
    }
}