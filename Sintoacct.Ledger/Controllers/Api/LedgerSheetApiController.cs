using System.Collections.Generic;
using System.Web.Http;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class LedgerSheetApiController : BaseApiController
    {
        private readonly ILedgerSheet _sheet;
        private readonly IModelValidation _modelValid;

        public LedgerSheetApiController(ILedgerSheet sheet,
                                        IModelValidation modelValid)
        {
            _sheet = sheet;
            _modelValid = modelValid;
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/LedgerSheet/GetMyPaymentTerms")]
        public IHttpActionResult GetMyPaymentTerms()
        {
            List<ComboboxViewModel> cb = new List<ComboboxViewModel>();
            var pt = _sheet.GetPaymentTerms();
            foreach(string s in pt)
            {
                if (string.IsNullOrEmpty(s)) continue;

                ComboboxViewModel cbvm = new ComboboxViewModel();
                cbvm.val = s;
                cbvm.text = string.Format("{0}年第{1}期",s.Substring(0,4),s.Substring(4));
                cb.Add(cbvm);
            }
            return Ok(cb);
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/LedgerSheet/GetMyAccountsInVoucher")]
        public IHttpActionResult GetMyAccountsInVoucher()
        {

            TreeViewModel<AccountViewModel> accountTree = _sheet.GetMyAccountsInVoucher();

            return Ok(accountTree.children);
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, HttpPost, Route("api/LedgerSheet/GetDetailSheet")]
        public IHttpActionResult GetDetailSheet(SearchConditionViewModel condition)
        { 

            List<DetailSheetViewModels> sheet = _sheet.GetDetailSheet(condition);
            DatagridViewModel<DetailSheetViewModels> dgSheet = new DatagridViewModel<DetailSheetViewModels>();
            dgSheet.rows = sheet;

            return Ok(dgSheet);
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet,HttpPost, Route("api/LedgerSheet/GetGeneralLedger")]
        public IHttpActionResult GetGeneralLedger(SearchConditionViewModel condition)
        {

            if(string.IsNullOrEmpty( condition.StartPeriod) || string.IsNullOrEmpty(condition.EndPeriod))
            {
                ResMessage.Fail("会计期间不能为空");
            }

            List<GeneralLedgerViewModels> sheet = _sheet.GetGeneralLedger(condition);
            DatagridViewModel<GeneralLedgerViewModels> dgSheet = new DatagridViewModel<GeneralLedgerViewModels>();
            dgSheet.rows = sheet;

            return Ok(dgSheet);
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, HttpPost, Route("api/LedgerSheet/GetAccountBalance")]
        public IHttpActionResult GetAccountBalance(SearchConditionViewModel condition)
        {

            if (string.IsNullOrEmpty(condition.StartPeriod) || string.IsNullOrEmpty(condition.EndPeriod))
            {
                ResMessage.Fail("会计期间不能为空");
            }

            List<AccountBalanceViewModels> sheet = _sheet.GetAccountBalance(condition);
            DatagridViewModel<AccountBalanceViewModels> dgSheet = new DatagridViewModel<AccountBalanceViewModels>();
            dgSheet.rows = sheet;

            return Ok(dgSheet);
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, HttpPost, Route("api/LedgerSheet/GetAccountBalance")]
        public IHttpActionResult GetMultiColumn(SearchConditionViewModel condition)
        {

            if (string.IsNullOrEmpty(condition.StartPeriod) || string.IsNullOrEmpty(condition.EndPeriod))
            {
                ResMessage.Fail("会计期间不能为空");
            }

            List<AccountBalanceViewModels> sheet = _sheet.GetAccountBalance(condition);
            DatagridViewModel<AccountBalanceViewModels> dgSheet = new DatagridViewModel<AccountBalanceViewModels>();
            dgSheet.rows = sheet;

            return Ok(dgSheet);
        }
    }
}