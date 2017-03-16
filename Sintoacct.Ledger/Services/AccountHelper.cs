using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class AccountHelper
    {
        private readonly LedgerContext _ledger;
        private readonly ICacheHelper _cache;
        private readonly IAccountBookHelper _acctBook;
        private readonly HttpContextBase _context;

        public AccountHelper(LedgerContext ledger, 
                             ICacheHelper cache, 
                             IAccountBookHelper acctBook, 
                             HttpContextBase context)
        {
            _ledger = ledger;
            _cache = cache;
            _acctBook = acctBook;
            _context = context;
        }

        #region AccountCategory
        public List<AccountCategory> GetMainAccountCategory()
        {
            return _ledger.AccountCategories.Where(ac => !ac.ParentAcId.HasValue).ToList();
        }

        public List<AccountCategory> GetSubAccountCategory(int mainCateId)
        {
            return _ledger.AccountCategories.Where(ac => ac.ParentAcId.HasValue && ac.ParentAcId.Value == mainCateId).ToList();
        }

        #endregion


        #region Account

        public Account GetAccount(long acctId)
        {
            return _ledger.Accounts.Where(a => a.AccId == acctId).FirstOrDefault();
        }

        public List<Account> GetAccountsOfCategory(int acctCateId)
        {
            AccountBook accBook = _acctBook.GetCurrentBook();
            if (accBook == null) return new List<Account>();

            return accBook.Accounts.Where(a => a.AccountCategory.ParentAcId == acctCateId || a.AccountCategory.AcId == acctCateId).ToList();
        }

        #endregion
    }
}