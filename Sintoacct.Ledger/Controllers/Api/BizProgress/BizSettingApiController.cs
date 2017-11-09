using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Sintoacct.Ledger.BizProgressServices;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class BizSettingApiController : BaseApiController
    {
        private readonly IBizSetting _setting;
        public BizSettingApiController(IBizSetting setting)
        {
            _setting = setting;
        }


        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/SaveBizCategory")]
        public IHttpActionResult SaveBizCategory(BizCategoryViewModel category)
        {

            try
            {
                _setting.SaveCategory(category);
            }
            catch(Exception err)
            {
                ResMessage.Fail(err.Message);
            }
            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/DeleteBizCategory")]
        public IHttpActionResult DeleteBizCategory(BizConfigDeleteViewModel category)
        {

            try
            {
                _setting.DeleteCategory(category.id);
            }
            catch(Exception err)
            {
                ResMessage.Fail(err.Message);
            }

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/SaveBizItem")]
        public IHttpActionResult SaveBizItem(BizItemViewModel item)
        {
            try
            {
                _setting.SaveBizItem(item);
            }
            catch(Exception err)
            {
                ResMessage.Fail(err.Message);
            }
            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/DeleteBizItem")]
        public IHttpActionResult DeleteBizItem(BizConfigDeleteViewModel item)
        {

            try
            {
                _setting.DeleteBizItem(item.id);
            }
            catch(Exception err)
            {
                ResMessage.Fail(err.Message);
            }

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/SaveBizStep")]
        public IHttpActionResult SaveBizStep(BizStepsViewModel step)
        {

            try
            {
                _setting.SaveBizStep(step);
            }
            catch(Exception err)
            {
                ResMessage.Fail(err.Message);
            }

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/DeleteBizStep")]
        public IHttpActionResult DeleteBizStep(BizConfigDeleteViewModel step)
        {
            try
            {
                _setting.DeleteBizStep(step.id);
            }
            catch(Exception err)
            {
                ResMessage.Fail(err.Message);
            }

            return Ok(ResMessage.Success());
        }

    }
}