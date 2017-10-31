using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using AutoMapper;
using System.Threading.Tasks;
using Sintoacct.Ledger.Common;
using Sintoacct.Ledger.BizProgressServices;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class BizProgressApiController : BaseApiController
    {
        private readonly IBizProgressService _progress;
        private readonly IModelValidation _modelValid;
        private const string _uploadPath = "~/uploads";

        public BizProgressApiController(IBizProgressService progress,
                                        IModelValidation modelValid)
        {
            _progress = progress;
            _modelValid = modelValid;
        }

        [ClaimsAuthorize("role", "business")]
        [HttpGet, HttpPost, Route("api/BizProgress/GetMyWorkOrders")]
        public IHttpActionResult GetMyWorkOrders()
        {
            var progList = _progress.GetMyWorkOrders();
            return Ok(Mapper.Map<List<WorkOrderViewModel>>(progList));
        }

        [ClaimsAuthorize("role", "business")]
        [HttpGet, HttpPost, Route("api/BizProgress/GetWorkProgress")]
        public IHttpActionResult GetWorkProgress(WorkProgressGetViewModel progGet)
        {
            var progList = _progress.GetWorkProgress(progGet.WoId, progGet.ItemId);
            return Ok(Mapper.Map<List<WorkProgressViewModel>>(progList));
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizProgress/SaveWorkOrder")]
        public IHttpActionResult SaveWorkOrder(WorkOrderViewModel progress)
        {
            string err;
            if(!_modelValid.ValidBizProgress(progress,out err))
            {
                ResMessage.Fail(err);
            }

            _progress.SaveWorkOrder(progress);
            
            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizProgress/DeleteWorkOrder")]
        public IHttpActionResult DeleteWorkOrder(WorkOrderDelViewModel workOrder)
        {
            if (workOrder.WoId<=0)
            {
                ResMessage.Fail("要删除的工单编号无效");
            }

            _progress.DeleteWorkOrder(workOrder);

            return Ok(ResMessage.Success());
        }


       

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizProgress/SaveWorkProgress")]
        public async Task<HttpResponseMessage> SaveWorkProgress()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath(_uploadPath);
            var provider = new MultipartFormDataStreamProvider(root);

            Dictionary<string, string> fileNames = new Dictionary<string, string>();
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    fileNames.Add(file.Headers.ContentDisposition.FileName, file.LocalFileName);
                }

                WorkProgressViewModel wProg = new WorkProgressViewModel();
                wProg.ProgId = Convert.ToInt64(HttpContext.Current.Request.Form["ProgId"]);
                wProg.StepId = Convert.ToInt32(HttpContext.Current.Request.Form["StepId"]);
                DateTime dt;
                if (DateTime.TryParse(HttpContext.Current.Request.Form["CompletedTime"], out dt)) wProg.CompletedTime = dt;
                wProg.ResultDesc = HttpContext.Current.Request.Form["ResultDesc"];
                decimal ae;
                if (decimal.TryParse(HttpContext.Current.Request.Form["AdvanceExpenditure"], out ae)) wProg.AdvanceExpenditure = ae;

                wProg.FileName = string.Format("{0}{1}",  System.IO.Path.GetFileName(fileNames.Values.FirstOrDefault()), System.IO.Path.GetExtension(fileNames.Keys.FirstOrDefault().Replace("\"",""))); 
                wProg.Url = string.Format("{0}/{1}", _uploadPath, System.IO.Path.GetFileName(wProg.FileName));
                _progress.SaveWorkProgress(wProg);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}