using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Sintoacct.Ledger.BizProgressServices;
using Sintoacct.Ledger.Models;
using AutoMapper;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class BizCustomerApiController : BaseApiController
    {
        private readonly IBizCustomer _customer;
        private readonly IModelValidation _modelValid;


        public BizCustomerApiController(IBizCustomer customer,
                                        IModelValidation modelValid)
        {
            _customer = customer;
            _modelValid = modelValid;
        }

        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizCustomer/SaveBizCustomer")]
        public IHttpActionResult SaveBizCustomer(BizCustomerViewModel customer)
        {
            

            _customer.SaveCustomer(customer);

            return Ok(ResMessage.Success());
        }


        [ClaimsAuthorize("role", "progress-record")]
        [HttpGet, HttpPost, Route("api/BizCustomer/Delete")]
        public IHttpActionResult DeleteCustomer(BizCustomerDelViewModel cusDel)
        {
            if (cusDel.CusId <= 0)
            {
                ResMessage.Fail("无效客户编号");
            }

            _customer.DeleteCustomer(cusDel.CusId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "business")]
        [HttpGet, HttpPost, Route("api/BizCustomer/SearchCustomers")]
        public IHttpActionResult SearchCustomers(BizCustomerConditionViewModel cusCondition)
        {
            var customers = _customer.GetCustomers(cusCondition);

            return Ok(Mapper.Map<List<BizCustomerViewModel>>(customers));
        }

    }
}