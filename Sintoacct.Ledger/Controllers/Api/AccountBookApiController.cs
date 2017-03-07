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
    public class AccountBookApiController : ApiController
    {
        private readonly IAccountBookHelper _acctBook;
        private readonly IModelValidation _modelValid;

        public AccountBookApiController(IAccountBookHelper acctBook,IModelValidation modelValid)
        {
            _acctBook = acctBook;
            _modelValid = modelValid;
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/MyAcctBook")]
        public IHttpActionResult GetMyAccountBook()
        {
            DatagridViewModels<AcctBookListViewModels> data = new DatagridViewModels<AcctBookListViewModels>();
            data.rows = Mapper.Map<List<AcctBookListViewModels>>(_acctBook.GetBooksOfUser());
            return Ok(data);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost,Route("api/acctbook/save"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAccountBook(AcctBookViewModels acctBook)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                return BadRequest(err);
            }

            if(!_modelValid.ValidAccountBookCreate(acctBook,out err))
            {
                ResMessage.Fail(err);
            }

            _acctBook.Save(acctBook);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/del"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAccountBook(AcctBookViewModels acctBook)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                return BadRequest(err);
            }

            if (!_modelValid.ValidAccountBookCreate(acctBook, out err))
            {
                ResMessage.Fail(err);
            }

            _acctBook.Delete(acctBook.AbId);

            return Ok(ResMessage.Success());
        }
    }
}