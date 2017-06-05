using Microsoft.AspNet.Identity;
using Sintoacct.Ledger.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace Sintoacct.Ledger.Services
{
    public class AccountBookHelper : IAccountBookHelper
    {
        private readonly ClaimsIdentity _identity;
        private readonly LedgerContext _ledger;
        private readonly ICacheHelper _cache;
        private readonly ICertificateWordHelper _certWord;

        public AccountBookHelper(HttpContextBase context, 
                                 LedgerContext ledger, 
                                 ICacheHelper cache,
                                 ICertificateWordHelper certWord)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _ledger = ledger;
            _cache = cache;
            _certWord = certWord;
        }

        private void AccountBookInitData(Guid newAbId)
        {
            //添加初始科目
            Guid abid = new Guid("00000000-0000-0000-0000-000000000000");

            List<Account> baseAccounts = _ledger.Accounts.Where(a => a.AbId == abid).ToList();

            foreach (Account acc in baseAccounts)
            {
                Account newAccount = new Account();
                newAccount.AccCode = acc.AccCode;
                newAccount.ParentAccCode = acc.ParentAccCode;
                newAccount.AcId = acc.AcId;
                newAccount.AccName = acc.AccName;
                newAccount.Direction = acc.Direction;
                newAccount.IsAuxiliary = false;
                newAccount.IsQuantity = false;
                newAccount.State = AccountState.Normal;
                newAccount.AbId = newAbId;
                newAccount.Creator = _identity.GetUserName();
                newAccount.CreateTime = DateTime.Now;

                _ledger.Accounts.Add(newAccount);
            }

            //添加初始凭证字
            CertificateWord defCertWord = new CertificateWord();
            defCertWord.CertWord = "记";
            defCertWord.PrintTitle = "记";
            defCertWord.AbId = newAbId;
            defCertWord.IsDefault = true;
            _ledger.CertificateWords.Add(defCertWord);

            //保存初始数据
            _ledger.SaveChanges();

        }

        public List<AccountBook> GetBooksOfUser()
        {
            string userId = _identity.GetUserId();
            List<UserBook> books = _ledger.UserBooks.Where(ub => ub.UserId == userId)
                                          .Include(ub => ub.AccountBook)
                                          .Include(ub => ub.AccountBook.Company).ToList();

            return books.Where(ub => ub.AccountBook.State == AccountBookState.Normal).Select(ub => ub.AccountBook).OrderByDescending(ub => ub.CreateTime).ToList();
        }

        public AccountBook GetAccountBook(Guid abid)
        {
            return _ledger.AccountBooks.Where(ab => ab.AbId == abid && ab.State== AccountBookState.Normal).Include("Accounts.AccountCategory").FirstOrDefault();
        }

        public AccountBook GetCurrentBook()
        {
            UserCacheModel userCache = _cache.GetUserCache();
            if (userCache == null) return null;

            return this.GetAccountBook(userCache.AccountBookID);
        }

        public AccountBook Save(AcctBookViewModels acctBook)
        {
            AccountBook book = null;
            bool isNew = false;
            if (string.IsNullOrEmpty(acctBook.AbId))
            {
                book = new AccountBook();
                book.Currency = acctBook.Currency;
                book.StartYear = acctBook.StartYear;
                book.StartPeriod = acctBook.StartPeriod;
                book.FiscalSystem = (FiscalSystem)acctBook.FiscalSystem;
                book.State = AccountBookState.Normal;

                book.Company = new Company();
                book.Company.ComName = acctBook.ComapnyName;
                //默认新公司都是南宁的
                book.Company.Region = _ledger.Regions.Where(r => r.RegionCode == 450100).FirstOrDefault();

                book.Creator = _identity.GetUserName();
                book.CreateTime = DateTime.Now;

                //创建人默认具有编辑账套内容的权限
                UserBook ub = new UserBook();
                ub.AccountBook = book;
                ub.UserId = _identity.GetUserId();
                book.Users.Add(ub);

                _ledger.AccountBooks.Add(book);

                isNew = true;
            }
            else
            {
                book = _ledger.AccountBooks.Where(ab => ab.AbId == Guid.Parse(acctBook.AbId)).FirstOrDefault();
                book.Currency = acctBook.Currency;
                book.FiscalSystem = (FiscalSystem)acctBook.FiscalSystem;

                isNew = false;
            }

            if (_ledger.SaveChanges() > 0 && isNew)
            {
                //初始化数据
                this.AccountBookInitData(book.AbId);
            }

            return book;
        }
    

        public void Delete(string abId)
        {
            Guid delId;
            if (!Guid.TryParse(abId, out delId)) throw new ArgumentNullException("账套编号无效");
            AccountBook book = _ledger.AccountBooks.Where(ab => ab.AbId == delId).FirstOrDefault();
            if(book!= null)
            {
                book.State = AccountBookState.Deleted;
                _ledger.SaveChanges();
            }
        }

    }


    public interface IAccountBookHelper : IDependency
    {

        List<AccountBook> GetBooksOfUser();

        AccountBook GetAccountBook(Guid abid);

        AccountBook GetCurrentBook();

        AccountBook Save(AcctBookViewModels acctBook);

        void Delete(string abId);
    }
}
