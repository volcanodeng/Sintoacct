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
            return Ok(_sheet.GetPaymentTerms());
        }
    }
}