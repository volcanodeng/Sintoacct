using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Sintoacct.Ledger.BizProgressServices;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class ReportApiController : BaseApiController
    {
        private readonly IReportService _report;

        public ReportApiController(IReportService report)
        {
            _report = report;
        }

        [ClaimsAuthorize("role", "report")]
        [HttpGet, HttpPost, Route("api/Report/GetProgressList")]
        public IHttpActionResult GetProgerssList(ProgressSearchViewModel condition)
        {
            var progs = _report.GetProgressList(condition);
            return Ok(progs);
        }
    }
}