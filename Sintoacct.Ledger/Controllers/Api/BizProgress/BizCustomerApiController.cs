using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Sintoacct.Ledger.BizProgressServices;
using Sintoacct.Ledger.Models;

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
            string err;
            if(!_modelValid.ValidBizCustomer(customer,out err))
            {
                ResMessage.Fail(err);
            }

            _customer.SaveCustomer(customer);

            return Ok(ResMessage.Success());
        }
    }
}