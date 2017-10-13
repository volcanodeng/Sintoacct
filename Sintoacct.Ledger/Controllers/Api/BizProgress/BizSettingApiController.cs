using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.BizProgressServices;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class BizSettingApiController : BaseApiController
    {
        private readonly IBizSetting _setting;
        public BizSettingApiController(IBizSetting setting)
        {
            _setting = setting;
        }
    }
}