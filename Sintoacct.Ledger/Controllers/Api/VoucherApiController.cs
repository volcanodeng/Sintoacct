using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;
using AutoMapper;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class VoucherApiController : ApiController
    {
        private readonly IModelValidation _modelValid;
        private readonly IVoucherHelper _voucher;

        public VoucherApiController(IModelValidation modelValid,IVoucherHelper voucher)
        {
            _modelValid = modelValid;
            _voucher = voucher;
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/voucher/myVoucher")]
        public IHttpActionResult GetMyVoucher(VoucherIdViewModel voucherId)
        {
            VoucherViewModel voucher = Mapper.Map<VoucherViewModel>(_voucher.GetMyVoucher(voucherId.VId));
            return Ok(voucher);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/saveVoucher"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveVoucher(VoucherViewModel voucher)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                return BadRequest(err);
            }

            _voucher.Save(voucher);

            return Ok(ResMessage.Success());
        }
    }
}