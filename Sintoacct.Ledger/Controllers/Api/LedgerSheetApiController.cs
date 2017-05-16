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
    public class LedgerSheetApiController : ApiController
    {
        private readonly ILedgerSheet _sheet;
        private readonly IModelValidation _modelValid;

        public LedgerSheetApiController(ILedgerSheet sheet,
                                        IModelValidation modelValid)
        {
            _sheet = sheet;
            _modelValid = modelValid;
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/LedgerSheet/GetMyPaymentTerms")]
        public IHttpActionResult GetMyPaymentTerms()
        {
            List<ComboboxViewModel> cb = new List<ComboboxViewModel>();
            var pt = _sheet.GetPaymentTerms();
            foreach(string s in pt)
            {
                ComboboxViewModel cbvm = new ComboboxViewModel();
                cbvm.val = s;
                cbvm.text = s;
                cb.Add(cbvm);
            }
            return Ok(cb);
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/LedgerSheet/GetMyAccountsInVoucher")]
        public IHttpActionResult GetMyAccountsInVoucher()
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                ResMessage.Fail(err);
            }

            TreeViewModel<AccountViewModel> accountTree = _sheet.GetMyAccountsInVoucher();

            return Ok(accountTree.children);
        }
    }
}