using Microsoft.AspNet.Identity;
using Sintoacct.Ledger.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System;


namespace Sintoacct.Ledger.Services
{
    public class AccountBookHelper : IAccountBookHelper
    {
        private ClaimsIdentity _identity;
        private readonly LedgerContext _ledger;

        public AccountBookHelper(HttpContextBase context,LedgerContext ledger)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _ledger = ledger;
        }

        public List<AccountBook> GetBooksOfUser()
        {
            string userId = _identity.GetUserId();
            List<UserBook> books = _ledger.UserBooks.Where(ub => ub.UserId == userId).Include(ub => ub.AccountBook).ToList();

            return books.Select(ub => ub.AccountBook).ToList();
        }

        public AccountBook Save(AcctBookViewModels acctBook)
        {
            AccountBook book = null;
            if (string.IsNullOrEmpty(acctBook.AbId))
            {
                book = new AccountBook();
                book.Currency = acctBook.Currency;
                book.StartYear = acctBook.StartYear;
                book.StartPeriod = acctBook.StartPeriod;
                book.FiscalSystem = (FiscalSystem)acctBook.FiscalSystem;

                book.Company = new Company();
                book.Company.ComName = acctBook.ComapnyName;
                //默认新公司都是南宁的
                book.Company.Region = _ledger.Regions.Where(r => r.RegionCode == 450100).FirstOrDefault();

                book.Creator = _identity.GetUserName();
                book.CreateTime = DateTime.Now;

                _ledger.AccountBooks.Add(book);
            }
            else
            {
                book = _ledger.AccountBooks.Where(ab => ab.AbId == Guid.Parse(acctBook.AbId)).FirstOrDefault();
                book.Currency = acctBook.Currency;
                book.FiscalSystem = (FiscalSystem)acctBook.FiscalSystem;
            }

            if (_ledger.SaveChanges() > 0) return book;

            return null;
        }

    }


    public interface IAccountBookHelper : IDependency
    {

        List<AccountBook> GetBooksOfUser();

        AccountBook Save(AcctBookViewModels acctBook);
    }
}
