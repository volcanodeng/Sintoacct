using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class AccountHelper : IAccountHelper
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

        public AccountCategory GetAccountCategory(int acId)
        {
            return _ledger.AccountCategories.Where(ac => ac.AcId == acId).FirstOrDefault();
        }

        #endregion


        #region Account

        private List<Account> GetAccountsWithAcctBookId(string acctBookId)
        {
            AccountCacheModel accounts = _cache.GetAccountCache(acctBookId);
            AccountBook accountBook = _acctBook.GetAccountBook(acctBookId);

            if (accountBook == null) throw new Exception("未找到账套");

            if (accounts == null)
            {
                accounts = new AccountCacheModel();
                accounts.AccountBookID = acctBookId;
                accounts.Accounts = accountBook.Accounts.ToList();
                _cache.SetAccountCache(accounts);
            }
            else
            {
                accounts.Accounts = accountBook.Accounts.ToList();
            }
            return accounts.Accounts;
        }

        public Account GetAccount(long acctId)
        {
            return _ledger.Accounts.Where(a => a.AccId == acctId).FirstOrDefault();
        }

        public List<Account> GetAccountsOfCategory(int acctCateId)
        {
            AccountBook accBook = _acctBook.GetCurrentBook();
            if (accBook == null) throw new Exception("未找到账套");

            List<Account> accounts = this.GetAccountsWithAcctBookId(accBook.AbId.ToString());

            return accounts.Where(a => a.AccountCategory.ParentAcId == acctCateId || a.AccountCategory.AcId == acctCateId).ToList();
        }

        public void SaveAccount(AccountViewModel vmAccount)
        {
            Account account = new Account();

            if (vmAccount.AccId > 0)
            {
                account = _ledger.Accounts.Where(a => a.AccId == vmAccount.AccId).FirstOrDefault();
            }
            else
            {
                account.AccCode = vmAccount.AccCode;
                account.ParentAccCode = vmAccount.ParentAccCode;

                AccountCategory Cate = this.GetAccountCategory(vmAccount.AcId);
                if (Cate == null) throw new Exception("科目类型为空");
                account.AccountCategory = Cate;

                account.State = AccountState.Normal;

                account.Creator = ((ClaimsIdentity)_context.User.Identity).GetUserName();
                account.CreateTime = DateTime.Now;

                _ledger.Accounts.Add(account);
            }

            account.AccName = vmAccount.AccName;
            account.Direction = vmAccount.Direction;
            account.IsAuxiliary = vmAccount.IsAuxiliary;
            if (account.IsAuxiliary)
            {
                account.AuxTypeIds = vmAccount.AuxTypeIds;
                account.AuxTypeNames = vmAccount.AuxTypeNames;
            }
            else
            {
                account.AuxTypeIds = string.Empty;
                account.AuxTypeNames = string.Empty;
            }
            account.IsQuantity = vmAccount.IsQuantity;
            if (account.IsQuantity)
            {
                account.Unit = vmAccount.Unit;
            }
            else
            {
                account.Unit = string.Empty;
            }

            _ledger.SaveChanges();
        }

        public void SaveAccountInitial(AccountViewModel vmAccount)
        {
            Account acct = GetAccount(vmAccount.AccId);
            if (acct == null) throw new ArgumentNullException("找不到科目");

            acct.InitialQuantity = vmAccount.InitialQuantity;
            acct.InitialBalance = vmAccount.InitialBalance;
            acct.YtdDebitQuantity = vmAccount.YtdDebitQuantity;
            acct.YtdDebit = vmAccount.YtdDebit;
            acct.YtdCreditQuantity = vmAccount.YtdCreditQuantity;
            acct.YtdCredit = vmAccount.YtdCredit;
            acct.YtdBeginBalanceQuantity = vmAccount.YtdBeginBalanceQuantity;
            acct.YtdBeginBalance = vmAccount.YtdBeginBalance;

            _ledger.SaveChanges();
        }

        #endregion
    }

    public interface IAccountHelper : IDependency
    {
        List<AccountCategory> GetMainAccountCategory();

        List<AccountCategory> GetSubAccountCategory(int mainCateId);

        AccountCategory GetAccountCategory(int acId);


        Account GetAccount(long acctId);

        List<Account> GetAccountsOfCategory(int acctCateId);

        void SaveAccount(AccountViewModel vmAccount);

        void SaveAccountInitial(AccountViewModel vmAccount);
    }
}