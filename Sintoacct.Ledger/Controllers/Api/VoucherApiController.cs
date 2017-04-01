﻿using System;
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
        public IHttpActionResult GetMyVoucher(long vid)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                ResMessage.Fail(err);
            }

            VoucherViewModel voucher = Mapper.Map<VoucherViewModel>(_voucher.GetMyVoucher(vid));
            return Ok(voucher);
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/voucher/myVouchers")]
        public IHttpActionResult GetMyVouchers()
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                ResMessage.Fail(err);
            }

            var voucher = Mapper.Map<List<VoucherViewModel>>(_voucher.GetMyVouchers());
            return Ok(voucher);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/saveVoucher"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveVoucher(VoucherViewModel voucher)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                ResMessage.Fail(err);
            }

            Voucher v = _voucher.Save(voucher);

            ResMessageContent rmContent = ResMessage.Success();
            rmContent.State = Mapper.Map<VoucherViewModel>(v);

            return Ok(rmContent);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/audit"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Audit(VoucherIdViewModel voucher)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                ResMessage.Fail(err);
            }

            try
            {
                _voucher.Audit(voucher.VId);
            }
            catch(Exception e)
            {
                ResMessage.Fail(e.Message);
            }

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/del"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteVoucher(VoucherIdViewModel voucher)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                ResMessage.Fail(err);
            }

            try
            {
                _voucher.Delete(voucher.VId);
            }
            catch (Exception e)
            {
                ResMessage.Fail(e.Message);
            }

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/voucher/myAbstracts")]
        public IHttpActionResult GetMyAbstracts()
        {
            var abstracts = Mapper.Map<List<AbstractViewModel>>(_voucher.GetMyAbstracts());
            return Ok(abstracts);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/saveAbstract"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAbstract(AbstractViewModel abs)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                ResMessage.Fail(err);
            }

            _voucher.SaveAbstract(abs);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/delAbstract"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAbstract(AbstractViewModel abs)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                ResMessage.Fail(err);
            }

            _voucher.DeleteAbstract(abs.AbsId);

            return Ok(ResMessage.Success());
        }
    }
}