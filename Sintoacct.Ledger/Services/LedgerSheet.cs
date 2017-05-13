using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class LedgerSheet : ILedgerSheet
    {
        private readonly LedgerContext _ledger;
        private readonly ICacheHelper _cache;

        public LedgerSheet(LedgerContext ledger,
                           ICacheHelper cache)
        {
            _ledger = ledger;
            _cache = cache;
        }

        #region 查询条件

        /// <summary>
        /// 获取当前用户账套内所有凭证的账期。
        /// 用于查询条件——账期
        /// </summary>
        /// <returns></returns>
        public List<string> GetPaymentTerms()
        {
            Guid abid = _cache.GetUserCache().AccountBookID;

            var terms = _ledger.Database.SqlQuery<string>("select [PaymentTerms] from [T_Voucher] where [AbId]=@abid group by [PaymentTerms]", new System.Data.SqlClient.SqlParameter("@abid", abid)).ToList();
            return terms;
        }

        #endregion

        #region 明细账

        /// <summary>
        /// 获取当前用户所有凭证的科目
        /// </summary>
        /// <returns></returns>
        public TreeViewModel<AccountViewModel> GetMyAccountsInVoucher()
        {
            Guid abid = _cache.GetUserCache().AccountBookID;
            List<Account> accounts = _ledger.Accounts.SqlQuery("select * from [T_Account] a where exists(select 1 from [T_Voucher] v, [T_Voucher_Detail] d where v.[VId]=d.[VId] and d.[AccId]=a.[AccId] and v.[AbId]=@abid)", new System.Data.SqlClient.SqlParameter("@abid", abid)).ToList();

            TreeViewModel<AccountViewModel> tree = new TreeViewModel<AccountViewModel>();
            tree.attributes = new AccountViewModel();
            tree.id = "-1";
            tree.text = "科目";
            tree.state = "open";

            Utility.AccountRecursion(accounts, tree);
            return tree;
        }

        #endregion
    }

    public interface ILedgerSheet : IDependency
    {
        List<string> GetPaymentTerms();

        TreeViewModel<AccountViewModel> GetMyAccountsInVoucher();
    }
}