using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Sintoacct.Ledger.BizProgressServices;
using AutoMapper;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ClaimsIdentity _identity;
        private readonly IBizCustomer _customer;

        public CustomerController(HttpContextBase context, IBizCustomer customer)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _customer = customer;
        }

        [ClaimsAuthorize("role", "business")]
        public ActionResult Customers()
        {
            return View();
        }

        [ClaimsAuthorize("role", "business")]
        public JsonResult GetCustomers()
        {
            return Json(Mapper.Map<List<BizCustomerViewModel>>(_customer.GetCustomers()), JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize("role", "business")]
        public ActionResult CostSetting()
        {
            return View();
        }

        [ClaimsAuthorize("role", "business")]
        public ActionResult MarketingChain()
        {
            return View();
        }
    }
}