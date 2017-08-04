using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sintoacct.Ledger.Controllers
{
    [ExceptionHandle]
    public class BaseController : Controller
    {
        //所有Controller都继承BaseController，则都会进行异常捕获
    }
}