using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Sintoacct.Ledger.BizProgressServices;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class BizProgressApiController : BaseApiController
    {
        private readonly IBizProgressService _progress;
        private readonly IModelValidation _modelValid;

        public BizProgressApiController(IBizProgressService progress,
                                        IModelValidation modelValid)
        {
            _progress = progress;
            _modelValid = modelValid;
        }

        [ClaimsAuthorize("role", "business")]
        [HttpGet, HttpPost, Route("api/BizProgress/GetMyBizProgresses")]
        public IHttpActionResult GetMyBizProgresses()
        {
            var progList = _progress.GetMyWorkOrders();
            return Ok(Mapper.Map<WorkOrderViewModel>(progList));
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizProgress/SaveBizProgress")]
        public IHttpActionResult SaveBizProgress(WorkOrderViewModel progress)
        {
            string err;
            if(!_modelValid.ValidBizProgress(progress,out err))
            {
                ResMessage.Fail(err);
            }

            _progress.SaveWorkOrder(progress);
            
            return Ok(ResMessage.Success());
        }
    }
}