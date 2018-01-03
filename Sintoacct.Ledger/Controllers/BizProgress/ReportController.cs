using System.Web.Mvc;


namespace Sintoacct.Ledger.Controllers
{
    public class ReportController : BaseController
    {

        [ClaimsAuthorize("role", "report")]
        public ActionResult ReportProgress()
        {
            return View();
        }

        
    }
}