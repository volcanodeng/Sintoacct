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
        private readonly ICertificateWordHelper _certWord;
        private readonly IAuxiliaryHelper _auxType;

        public AccountBookApiController(IAccountBookHelper acctBook,
                                        ICertificateWordHelper certWord,
                                        IModelValidation modelValid,
                                        IAuxiliaryHelper auxType)
        {
            _acctBook = acctBook;
            _modelValid = modelValid;
            _certWord = certWord;
            _auxType = auxType;
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

            _acctBook.Delete(acctBook.AbId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/MyCertWord")]
        public IHttpActionResult GetCertWord()
        {
            DatagridViewModels<CertWordViewModel> data = new DatagridViewModels<CertWordViewModel>();
            data.rows = Mapper.Map<List<CertWordViewModel>>(_certWord.GetCertWordInAccountBook());
            return Ok(data);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/saveCertword"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveCertWord(CertWordViewModel certWord)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                return BadRequest(err);
            }

            _certWord.Save(certWord);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/delCertword"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteCertWord(CertWordDeleteViewModel certWord)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                return BadRequest(err);
            }

            if(certWord == null)
            {
                ResMessage.Fail("传入模型为空");
            }

            _certWord.Delete(certWord.CwId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/setCwDef"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SetCertWordDefault(CertWordDeleteViewModel certWord)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                return BadRequest(err);
            }

            _certWord.SetDefault(certWord.CwId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/addAuxType"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult AddAuxiliaryType(AuxiliaryTypeViewModel auxType)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                return BadRequest(err);
            }

            _auxType.Add(auxType.AuxType);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/delAuxType"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAuxiliaryType(AuxiliaryTypeViewModel auxType)
        {
            string err;
            if (!_modelValid.Valid(ModelState, out err))
            {
                return BadRequest(err);
            }

            _auxType.Delete(auxType.AtId);

            return Ok(ResMessage.Success());
        }

        
    }
}