using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Sintoacct.Ledger.Models;
using AutoMapper;

namespace Sintoacct.Ledger.Services
{
    public class AccountHelper : IAccountHelper
    {
        private readonly LedgerContext _ledger;
        private readonly ICacheHelper _cache;
        private readonly IAccountBookHelper _acctBook;
        private readonly HttpContextBase _context;
        private readonly IAuxiliaryHelper _auxiliary;

        public AccountHelper(LedgerContext ledger, 
                             ICacheHelper cache, 
                             IAccountBookHelper acctBook, 
                             HttpContextBase context,
                             IAuxiliaryHelper auxiliary)
        {
            _ledger = ledger;
            _cache = cache;
            _acctBook = acctBook;
            _context = context;
            _auxiliary = auxiliary;
        }

        private void Recursion(List<Account> accounts,TreeViewModel<AccountViewModel> tree)
        {
            foreach(Account a in accounts)
            {
                TreeViewModel<AccountViewModel> accNode = new TreeViewModel<AccountViewModel>();
                accNode.id = a.AccId.ToString();
                accNode.text = a.AccName;
                accNode.state = "open";
                accNode.@checked = false;
                accNode.attributes = Mapper.Map<AccountViewModel>(a);
                tree.children.Add(accNode);

                Recursion(accounts.Where(acc => acc.ParentAccCode == a.AccCode).ToList(), accNode);
            }

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

        public List<int> GetAccountCategoriesWithQuantity()
        {
            List<int> c = new List<int>();
            List<AccountCategory> mainCate = GetMainAccountCategory();
            foreach(AccountCategory cate in mainCate)
            {
                if (this.GetAccountsOfCategory(cate.AcId).Any(a => a.IsQuantity)) c.Add(cate.AcId);
            }
            return c;
        }

        #endregion


        #region Account

        private List<Account> GetAccountsWithAcctBookId()
        {
            AccountCacheModel accounts = _cache.GetAccountCache(_cache.GetUserCache().AccountBookID);

            if (accounts == null)
            {
                AccountBook accountBook = _acctBook.GetCurrentBook();
                if (accountBook == null) throw new Exception("未找到账套");

                accounts = new AccountCacheModel();
                accounts.AccountBookID = accountBook.AbId;
                accounts.Accounts = accountBook.Accounts.ToList();
                _cache.SetAccountCache(accounts);
            }
            
            return accounts.Accounts;
        }

        public Account GetAccount(long acctId)
        {
            return _ledger.Accounts.Where(a => a.AccId == acctId).FirstOrDefault();
        }

        public List<Account> GetAccountsOfCategory(int acctCateId)
        {
            List<Account> accounts = this.GetAccountsWithAcctBookId();

            return accounts.Where(a => a.AccountCategory.ParentAcId == acctCateId || a.AccountCategory.AcId == acctCateId).OrderBy(a=>a.AccCode).ToList();
        }

        public TreeViewModel<AccountViewModel> GetAccountTreeOfCategory(int acctCateId)
        {
            List<Account> cAccount = GetAccountsOfCategory(acctCateId);
            TreeViewModel<AccountViewModel> tree = new TreeViewModel<AccountViewModel>();

            this.Recursion(cAccount.Where(a => a.ParentAccCode == null || a.ParentAccCode == string.Empty).ToList(), tree);
            return tree;
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

                

                account.State = AccountState.Normal;

                account.Creator = ((ClaimsIdentity)_context.User.Identity).GetUserName();
                account.CreateTime = DateTime.Now;

                _ledger.Accounts.Add(account);
            }

            AccountCategory Cate = this.GetAccountCategory(vmAccount.AcId);
            if (Cate == null) throw new Exception("科目类型为空");
            account.AccountCategory = Cate;

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

            _cache.ClearAccountCache(_cache.GetUserCache().AccountBookID);
        }

        public void SaveAccountInitial(List<AccountViewModel> vmAccounts)
        {
            foreach (AccountViewModel acc in vmAccounts)
            {
                Account acct = GetAccount(acc.AccId);
                if (acct == null) throw new ArgumentNullException("找不到科目");

                acct.InitialQuantity = acc.InitialQuantity;
                acct.InitialBalance = acc.InitialBalance;
                acct.YtdDebitQuantity = acc.YtdDebitQuantity;
                acct.YtdDebit = acc.YtdDebit;
                acct.YtdCreditQuantity = acc.YtdCreditQuantity;
                acct.YtdCredit = acc.YtdCredit;
                acct.YtdBeginBalanceQuantity = acct.InitialQuantity - acct.YtdDebitQuantity + acct.YtdCreditQuantity;
                acct.YtdBeginBalance = acct.InitialBalance - acct.YtdDebit - acct.YtdCredit;
            }

            _ledger.SaveChanges();

            _cache.ClearAccountCache(_cache.GetUserCache().AccountBookID);
        }

        public void DeleteAccount(long acctId)
        {
            Account acc = GetAccount(acctId);
            if (acc == null) throw new ArgumentNullException("找不到要删除的科目");

            _ledger.Accounts.Remove(acc);
            _ledger.SaveChanges();

            _cache.ClearAccountCache(_cache.GetUserCache().AccountBookID);
        }

        public void AddAuxAccount(AuxiliaryAccountViewModel vmAuxAccount)
        {
            Account auxAccount = this.GetAccount(vmAuxAccount.AccId);
            _ledger.Entry(auxAccount).State = System.Data.Entity.EntityState.Detached;
            auxAccount.AccId = 0;

            List<Auxiliary> auxList = new List<Auxiliary>();
            if(vmAuxAccount.Custom.HasValue && vmAuxAccount.Custom.Value>0)
            {
                auxList.Add(_auxiliary.GetAuxiliary(vmAuxAccount.Custom.Value));
            }
            if (vmAuxAccount.Suppliers.HasValue && vmAuxAccount.Suppliers.Value > 0)
            {
                auxList.Add(_auxiliary.GetAuxiliary(vmAuxAccount.Suppliers.Value));
            }
            if (vmAuxAccount.Employee.HasValue && vmAuxAccount.Employee.Value > 0)
            {
                auxList.Add(_auxiliary.GetAuxiliary(vmAuxAccount.Employee.Value));
            }
            if (vmAuxAccount.Project.HasValue && vmAuxAccount.Project.Value > 0)
            {
                auxList.Add(_auxiliary.GetAuxiliary(vmAuxAccount.Project.Value));
            }
            if (vmAuxAccount.Sector.HasValue && vmAuxAccount.Sector.Value > 0)
            {
                auxList.Add(_auxiliary.GetAuxiliary(vmAuxAccount.Sector.Value));
            }
            if (vmAuxAccount.Inventory.HasValue && vmAuxAccount.Inventory.Value > 0)
            {
                auxList.Add(_auxiliary.GetAuxiliary(vmAuxAccount.Inventory.Value));
            }

            foreach(Auxiliary aux in auxList)
            {
                auxAccount.AccCode += "_" + aux.AuxCode;
                auxAccount.AccName += "_" + aux.AuxName;
            }

            _ledger.Accounts.Add(auxAccount);
            _ledger.SaveChanges();

            _cache.ClearAccountCache(_cache.GetUserCache().AccountBookID);
        }

        /// <summary>
        /// 科目期初试算平衡
        /// </summary>
        public List<TrialBalanceViewModel> TrialBalance()
        {
            List<Account> accounts = GetAccountsWithAcctBookId();

            var initBal = from a in accounts
                          group a by a.Direction into g
                          select new { g.Key, InitialBalance = g.Sum(a => a.InitialBalance) };

            TrialBalanceViewModel tbInit = new TrialBalanceViewModel();
            tbInit.ItemName = "期初余额";
            tbInit.DebitBalance = initBal.Where(a => a.Key == "借").FirstOrDefault().InitialBalance ?? 0;
            tbInit.CreditBalance = initBal.Where(a => a.Key == "贷").FirstOrDefault().InitialBalance ?? 0;
            tbInit.Imbalance = tbInit.DebitBalance - tbInit.CreditBalance;

            TrialBalanceViewModel tbYtd = new TrialBalanceViewModel();
            tbYtd.ItemName = "累计发生额";
            tbYtd.DebitBalance = accounts.Sum(a => a.YtdDebit) ?? 0;
            tbYtd.CreditBalance = accounts.Sum(a => a.YtdCredit) ?? 0;
            tbYtd.Imbalance = tbYtd.DebitBalance - tbYtd.CreditBalance;

            return new List<TrialBalanceViewModel>() { tbInit, tbYtd };
        }

        #endregion
    }

    public interface IAccountHelper : IDependency
    {
        List<AccountCategory> GetMainAccountCategory();

        List<AccountCategory> GetSubAccountCategory(int mainCateId);

        TreeViewModel<AccountViewModel> GetAccountTreeOfCategory(int acctCateId);

        AccountCategory GetAccountCategory(int acId);

        List<int> GetAccountCategoriesWithQuantity();




        Account GetAccount(long acctId);

        List<Account> GetAccountsOfCategory(int acctCateId);

        void SaveAccount(AccountViewModel vmAccount);

        void SaveAccountInitial(List<AccountViewModel> vmAccounts);

        void DeleteAccount(long acctId);

        void AddAuxAccount(AuxiliaryAccountViewModel vmAuxAccount);

        List<TrialBalanceViewModel> TrialBalance();
    }
}