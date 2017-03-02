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

        //[Authorize(Roles = "abc@qq.com")]
        [ClaimsAuthorize("role", "admin","guest1")]
        public ActionResult Index()
        {
            return View(_acctBook.GetLedger());
        }
    }
}