using System.Web.Mvc;
using Sintoacct.Ledger.Services;

namespace Sintoacct.Ledger.Controllers
{
    public class LedgerController : Controller
    {
        private readonly IAccountBookHelper _acctBook;

        public LedgerController(IAccountBookHelper acctBook)
        {
            _acctBook = acctBook;
        }


        [ClaimsAuthorize("role", "accountant")]
        public ActionResult AccountBook()
        {
            return View();
        }
    }
}