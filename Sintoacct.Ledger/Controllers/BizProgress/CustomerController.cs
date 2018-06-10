using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Sintoacct.Ledger.BizProgressServices;
using AutoMapper;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Common;

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
            return Json(Mapper.Map<List<BizCustomerViewModel>>(_customer.GetCustomers()), "text/html", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetValidCustomers()
        {
            return Json(Mapper.Map<List<BizCustomerViewModel>>(_customer.GetValidCustomers()), "text/html", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerLevel()
        {
            return Json(EnumJson.Convert(typeof(Sintoacct.Progress.Models.CustomerLevel)),"text/html", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerState()
        {
            EnumJson[] cState = EnumJson.Convert(typeof(Sintoacct.Progress.Models.CustomerState));
            foreach(EnumJson s in cState)
            {
                if (s.Name == "Normal") s.Name = "正常";
                if (s.Name == "Stopped") s.Name = "欠费";
                if (s.Name == "Canceled") s.Name = "已注销";
                if (s.Name == "Deleted") s.Name = "已删除";
                if (s.Name == "LostContact") s.Name = "已失联";
                if (s.Name == "Transferred") s.Name = "已转走";
            }
            return Json(cState, "text/html", JsonRequestBehavior.AllowGet);
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