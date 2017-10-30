﻿using System;
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
        public IHttpActionResult SaveWorkProgress(WorkProgressViewModel progress)
        {
            this.PostFile();

            _progress.SaveWorkProgress(progress);

            return Ok(ResMessage.Success());
        }


        private void PostFile()
        {
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data")) return;

            var provider = new MultipartFormDataStreamProvider(HttpContext.Current.Server.MapPath("~/uploads"));
            var bodyPart =  Request.Content.ReadAsMultipartAsync(provider).Result;
            
        }

        [HttpGet, HttpPost, Route("api/BizProgress/PostFormData")]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/uploads");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    //Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    //Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}