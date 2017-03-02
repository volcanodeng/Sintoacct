using System.Web.Mvc;

namespace Sintoacct.Ledger.Controllers
{
    public class LedgerController : Controller
    {
        private readonly IAccountBookHelper _acctBook;

        public LedgerController(IAccountBookHelper acctBook)
        {
            _acctBook = acctBook;
        }

        //测试
        [ClaimsAuthorize("role", "admin","guest")]
        public ActionResult Test()
        {
            return View(_acctBook.GetLedger());
        }

        [ClaimsAuthorize("role", "accountant")]
        public ActionResult AccountBook()
        {
            return View();
        }
    }
}