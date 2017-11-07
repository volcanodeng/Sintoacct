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


            _setting.SaveCategory(category);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/DeleteBizCategory")]
        public IHttpActionResult DeleteBizCategory(BizConfigDeleteViewModel category)
        {


            _setting.DeleteCategory(category.id);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/SaveBizItem")]
        public IHttpActionResult SaveBizItem(BizItemViewModel item)
        {


            _setting.SaveBizItem(item);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/DeleteBizItem")]
        public IHttpActionResult DeleteBizItem(BizConfigDeleteViewModel item)
        {


            _setting.DeleteBizItem(item.id);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/SaveBizStep")]
        public IHttpActionResult SaveBizStep(BizStepsViewModel step)
        {


            _setting.SaveBizStep(step);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizSetting/DeleteBizStep")]
        public IHttpActionResult DeleteBizStep(BizConfigDeleteViewModel step)
        {


            _setting.DeleteBizStep(step.id);

            return Ok(ResMessage.Success());
        }

    }
}