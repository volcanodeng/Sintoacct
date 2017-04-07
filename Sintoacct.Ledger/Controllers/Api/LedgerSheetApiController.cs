using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;
using AutoMapper;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class LedgerSheetApiController : ApiController
    {
        private readonly ILedgerSheet _sheet;
        public LedgerSheetApiController(ILedgerSheet sheet)
        {
            _sheet = sheet;
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/LedgerSheet/GetMyPaymentTerms")]
        public IHttpActionResult GetMyPaymentTerms()
        {
            List<ComboboxViewModel> cb = new List<ComboboxViewModel>();
            var pt = _sheet.GetPaymentTerms();
            foreach(string s in pt)
            {
                ComboboxViewModel cbvm = new ComboboxViewModel();
                cbvm.val = s;
                cbvm.text = s;
                cb.Add(cbvm);
            }
            return Ok(cb);
        }
    }
}