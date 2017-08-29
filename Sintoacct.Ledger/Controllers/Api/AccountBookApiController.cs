using System.Collections.Generic;
using System.Web.Http;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;
using AutoMapper;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class AccountBookApiController : BaseApiController
    {
        private readonly IAccountBookHelper _acctBook;
        private readonly IModelValidation _modelValid;
        private readonly ICertificateWordHelper _certWord;
        private readonly IAuxiliaryHelper _auxType;
        private readonly IAccountHelper _account;

        public AccountBookApiController(IAccountBookHelper acctBook,
                                        ICertificateWordHelper certWord,
                                        IModelValidation modelValid,
                                        IAuxiliaryHelper auxType,
                                        IAccountHelper account)
        {
            _acctBook = acctBook;
            _modelValid = modelValid;
            _certWord = certWord;
            _auxType = auxType;
            _account = account;
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/MyAcctBook")]
        public IHttpActionResult GetMyAccountBook()
        {
            DatagridViewModel<AcctBookListViewModels> data = new DatagridViewModel<AcctBookListViewModels>();
            data.rows = Mapper.Map<List<AcctBookListViewModels>>(_acctBook.GetBooksOfUser());
            return Ok(data);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost,Route("api/acctbook/save"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAccountBook(AcctBookViewModels acctBook)
        {
            string err;

            if(!_modelValid.ValidAccountBookCreate(acctBook,out err))
            {
                ResMessage.Fail(err);
            }

            _acctBook.Save(acctBook);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/del"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAccountBook(AcctBookDelViewModel acctBook)
        {
            
            _acctBook.Delete(acctBook.AbId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/MyCertWord")]
        public IHttpActionResult GetCertWord()
        {
            DatagridViewModel<CertWordViewModel> data = new DatagridViewModel<CertWordViewModel>();
            data.rows = Mapper.Map<List<CertWordViewModel>>(_certWord.GetCertWordInAccountBook());
            return Ok(data);
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/MyCertWordArray")]
        public IHttpActionResult GetCertWordArray()
        {
            List<CertWordViewModel> certWords = Mapper.Map<List<CertWordViewModel>>(_certWord.GetCertWordInAccountBook());
            return Ok(certWords);
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/saveCertword"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveCertWord(CertWordViewModel certWord)
        {

            _certWord.Save(certWord);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/delCertword"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteCertWord(CertWordDeleteViewModel certWord)
        {

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

            _certWord.SetDefault(certWord.CwId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/addAuxType"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult AddAuxiliaryType(AuxiliaryTypeViewModel auxType)
        {

            _auxType.Add(auxType.AuxType);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/delAuxType"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAuxiliaryType(AuxiliaryTypeViewModel auxType)
        {

            _auxType.Delete(auxType.AtId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/saveAux"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAuxiliary(AuxiliaryViewModel vmAux)
        {

            _auxType.SaveAuxiliary(vmAux);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/delAux"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAuxiliary(AuxiliaryDeleteViewModel delAux)
        {

            _auxType.DeleteAuxiliary(delAux.AuxId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/GetAuxOfType")]
        public IHttpActionResult GetAuxiliaryOfType(int auxTypeId)
        {

            List<Auxiliary> auxList = _auxType.GetAuxiliaryOfType(auxTypeId);

            DatagridViewModel<AuxiliaryViewModel> auxDg = new DatagridViewModel<AuxiliaryViewModel>();
            auxDg.rows = Mapper.Map<List<AuxiliaryViewModel>>(auxList);

            return Ok(auxDg);
        }


        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/saveAccount"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAccount(AccountViewModel vmAccount)
        {
            
            _account.SaveAccount(vmAccount);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/saveAccountInit"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAccountInitial(AccountInitViewModel vmAccount)
        {
            

            _account.SaveAccountInitial(vmAccount.Accounts);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/accountofcate")]
        public IHttpActionResult GetAccountsOfCategory(int acctCateId)
        {

            List<Account> accList = _account.GetAccountsOfCategory(acctCateId);

            DatagridViewModel<AccountViewModel> accDg = new DatagridViewModel<AccountViewModel>();
            accDg.rows = Mapper.Map<List<AccountViewModel>>(accList);

            return Ok(accDg);
        }

        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/accTreeOfCate")]
        public IHttpActionResult GetAccountTreeOfCategory(int acctCateId)
        {

            TreeViewModel<AccountViewModel> accountTree = acctCateId>0 ? _account.GetAccountTreeOfCategory(acctCateId) : _account.GetAccountTree();

            return Ok(accountTree.children);
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/subAccCate")]
        public IHttpActionResult GetSubAccountCategory(int mainCateId)
        {

            List<AccountCategory> accCateList = _account.GetSubAccountCategory(mainCateId);

            DatagridViewModel<AccountCategoryViewModel> accDg = new DatagridViewModel<AccountCategoryViewModel>();
            accDg.rows = Mapper.Map<List<AccountCategoryViewModel>>(accCateList);

            return Ok(accDg.rows);
        }


        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/delAccount"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAccount(AccountDeleteViewModel vmAccount)
        {

            if(vmAccount==null)
            {
                ResMessage.Fail("参数为空");
            }

            _account.DeleteAccount(vmAccount.AccId);

            return Ok(ResMessage.Success());
        }

        [ClaimsAuthorize("role", "accountant-edit")]
        [HttpPost, Route("api/acctbook/addAuxAccount"), System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult AddAuxAccount(AuxiliaryAccountViewModel vmAuxAccount)
        {

            _account.AddAuxAccount(vmAuxAccount);

            return Ok(ResMessage.Success());
        }


        [ClaimsAuthorize("role", "accountant")]
        [HttpGet, Route("api/acctbook/TrialBalance")]
        public IHttpActionResult GetTrialBalance()
        {

            DatagridViewModel<TrialBalanceViewModel> TrialBalance = new DatagridViewModel<TrialBalanceViewModel>();
            TrialBalance.rows = _account.TrialBalance();

            return Ok(TrialBalance);
        }

    }
}