using AutoMapper;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Sintoacct.Ledger.Controllers.Api
{

    public class VoucherApiController : BaseApiController
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
            VoucherViewModel voucher = Mapper.Map<VoucherViewModel>(_voucher.GetMyVoucher(vid));
            return Ok(voucher);
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/voucher/myVouchers")]
        public IHttpActionResult GetMyVouchers()
        {

            var voucher = Mapper.Map<List<VoucherViewModel>>(_voucher.GetMyUnauditVouchers(10));
            return Ok(voucher);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/saveVoucher"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveVoucher(VoucherViewModel voucher)
        {
            string err;

            //校验借贷是否平衡、凭证字号是否最新、科目是否有效等
            if(!_modelValid.ValidVoucher(voucher,out err))
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
            
            try
            {
                _voucher.Audit(voucher.VId,voucher.ReviewOpinion);
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


        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/SetInvoice"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SetInvoicePath(VoucherInvoicePathModel invoice)
        {

            try
            {
                _voucher.SetInvoicePath(invoice.VId, invoice.InvoicePath);
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

            _voucher.SaveAbstract(abs);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/voucher/delAbstract"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAbstract(AbstractViewModel abs)
        {

            _voucher.DeleteAbstract(abs.AbsId);

            return Ok(ResMessage.Success());
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpPost, Route("api/voucher/viewVoucher")]
        public IHttpActionResult ViewVoucher(SearchConditionViewModel condition)
        {

            var voucher = _voucher.VoucherToSearchVoucherViewModel(_voucher.SearchVoucher(condition));
            DatagridViewModel<SearchVoucherViewModel> dgsv = new DatagridViewModel<SearchVoucherViewModel>();
            dgsv.rows = voucher;
            return Ok(dgsv);
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/voucher/GetNewCertWordSn")]
        public IHttpActionResult GetNewCertWordSn(DateTime vDate, int cwId)
        {
            var newSn = _voucher.GetMaxCertWordSn(vDate, cwId) + 1;
            return Ok(newSn);
        }
    }
}