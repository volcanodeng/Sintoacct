using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace Sintoacct.Ledger.Controllers.BizProgress
{
    public class BizSettingController : BaseController
    {
        private readonly ClaimsIdentity _identity;

        public BizSettingController(HttpContextBase context)
        {
            _identity = context.User.Identity as ClaimsIdentity;
        }
    }
}