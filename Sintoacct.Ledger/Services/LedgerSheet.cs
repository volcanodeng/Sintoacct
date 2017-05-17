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
            string sql = string.Format("select * from [T_Account] a where exists(select 1 from [T_Voucher] v, [T_Voucher_Detail] d where v.[VId]=d.[VId] and d.[AccId]=a.[AccId] and v.[AbId]={0})",Utility.ParameterNameString("abid"));
            List<Account> accounts = _ledger.Accounts.SqlQuery(sql, Utility.NewParameter("abid", abid)).ToList();

            TreeViewModel<AccountViewModel> tree = new TreeViewModel<AccountViewModel>();
            tree.attributes = new AccountViewModel();
            tree.id = "-1";
            tree.text = "科目";
            tree.state = "open";

            Utility.AccountRecursion(accounts, tree);
            return tree;
        }

        /// <summary>
        /// 生成明细账列表数据。
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public List<DetailSheetViewModels> GetDetailSheet(long accid)
        {
            Guid abid = _cache.GetUserCache().AccountBookID;
            string sql = "select v.VoucherDate,cw.CertWord+'-'+CONVERT(nvarchar(10),v.certwordsn) as CertWord,vd.Abstract,vd.Debit,vd.Credit,a.Direction,0 as Balance " +
                         "from T_Voucher v inner join T_Voucher_Detail vd on v.VId=vd.VId " +
                         "left join T_Certificate_Word cw on v.CertificateWord_CwId=cw.CwId " +
                         "left join T_Account a on a.AccId=vd.AccId " +
                         string.Format("where v.AbId={0} and vd.AccId={1} ", Utility.ParameterNameString("abid"), Utility.ParameterNameString("accid"))+
                         "order by vd.VdId";

            List<DetailSheetViewModels> sheets = _ledger.Database.SqlQuery<DetailSheetViewModels>(sql, Utility.NewParameter("abid", abid), Utility.NewParameter("accid", accid)).ToList();

            decimal balance = 0;
            foreach(DetailSheetViewModels s in sheets)
            {
                balance += (s.Direction == "借" ? s.Debit : s.Credit);
                s.Balance = balance;
            }

            return sheets;
        }

        #endregion
    }

    public interface ILedgerSheet : IDependency
    {
        List<string> GetPaymentTerms();

        TreeViewModel<AccountViewModel> GetMyAccountsInVoucher();

        List<DetailSheetViewModels> GetDetailSheet(long accid);
    }
}