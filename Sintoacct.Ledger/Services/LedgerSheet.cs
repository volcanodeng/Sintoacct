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
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<DetailSheetViewModels> GetDetailSheet(SearchConditionViewModel condition)
        {
            Guid abid = _cache.GetUserCache().AccountBookID;
            object[] parames = new object[] {
                Utility.NewParameter("abid", abid),
                Utility.NewParameter("startterm", condition.StartPeriod),
                Utility.NewParameter("endterm", condition.EndPeriod),
                Utility.NewParameter("accid", condition.AccId)
            };

            List<DetailSheetViewModels> detailSheet = new List<DetailSheetViewModels>();
            //获取查询的所有会计期间
            List<string> paymentTerms = _ledger.Database.SqlQuery<string>(string.Format("select PaymentTerms from T_Voucher where AbId = {0} and {1} <= PaymentTerms and PaymentTerms <= {2} group by PaymentTerms order by PaymentTerms",
                                                                                         Utility.ParameterNameString("abid"), Utility.ParameterNameString("startterm"), Utility.ParameterNameString("endterm")),
                                                                          parames).ToList();

            #region 期初余额
            //计算期初发生余额
            string initBalance = "select " +
                                    " GETDATE() as VoucherDate," +
                                    " '' as CertWord," +
                                    " '期初余额' as Abstract,"+
                                    " sum(vd.Debit) as Debit,"+
                                    " sum(vd.Credit) as Credit,"+
                                    " '平' as Direction," +
                                    "min(v.PaymentTerms) as PaymentTerms," +
                                    " case a.Direction when '借' then sum(vd.Debit)-sum(vd.Credit) when '贷' then sum(vd.Credit)-sum(vd.Debit) end as Balance " +
                                    " from T_Voucher v inner join T_Voucher_Detail vd on v.VId=vd.VId " +
                                    " left join T_Account a on a.AccId=vd.AccId " +
                                    string.Format(" where v.AbId={0} and vd.AccId={1} ", Utility.ParameterNameString("abid"), Utility.ParameterNameString("accid")) +
                                    string.Format(" and v.PaymentTerms < {0} ", Utility.ParameterNameString("startterm")) +
                                    " group by  vd.AccId,a.Direction";

            Account account = _ledger.Accounts.Where(a => a.AccId == condition.AccId).FirstOrDefault();
            DetailSheetViewModels initDetail = _ledger.Database.SqlQuery<DetailSheetViewModels>(initBalance, parames.Select(p => ((ICloneable)p).Clone()).ToArray()).FirstOrDefault();
            if (initDetail == null)
            {
                initDetail = new DetailSheetViewModels();
                initDetail.Abstract = "期初余额";
                initDetail.Direction = "平";
            }
            initDetail.VoucherDate = new DateTime(Convert.ToInt32(condition.StartPeriod.Substring(0, 4)), Convert.ToInt32(condition.StartPeriod.Substring(4)), 1);
            initDetail.PaymentTerms = condition.StartPeriod;

            if (account != null && account.InitialBalance > 0)
            {
                //加上科目余额
                if (account.Direction == "借")
                {
                    initDetail.Debit += account.InitialBalance;
                }
                if (account.Direction == "贷")
                {
                    initDetail.Credit += account.InitialBalance;
                }
            }
            //期初余额方向，默认显示“平”
            if (initDetail.Debit > 0 || initDetail.Credit > 0) initDetail.Direction = account.Direction;
            //加入集合
            detailSheet.Add(initDetail);
            #endregion

            #region 明细
            //各期的明细
            string detail = "select " +
                                    " v.VoucherDate," +
                                    " cw.CertWord+'-'+CONVERT(nvarchar(10),v.certwordsn) as CertWord," +
                                    " vd.Abstract," +
                                    " vd.Debit," +
                                    " vd.Credit," +
                                    " a.Direction," +
                                    " v.PaymentTerms," +
                                    " 0.00 as Balance " +
                                    " from T_Voucher v inner join T_Voucher_Detail vd on v.VId=vd.VId " +
                                    " left join T_Account a on a.AccId=vd.AccId " +
                                    " left join T_Certificate_Word cw on v.CertificateWord_CwId=cw.CwId " +
                                    string.Format(" where v.AbId={0} and vd.AccId={1} ", Utility.ParameterNameString("abid"), Utility.ParameterNameString("accid")) +
                                    string.Format(" and {0} <= v.PaymentTerms and v.PaymentTerms <= {1} ", Utility.ParameterNameString("startterm"), Utility.ParameterNameString("endterm"))+
                                    " order by v.VoucherYear,v.VoucherMonth";

            //计算明细余额
            var detailRecord = _ledger.Database.SqlQuery<DetailSheetViewModels>(detail, parames.Select(p => ((ICloneable)p).Clone()).ToArray()).ToList();
            decimal balance = initDetail.Balance;
            foreach(DetailSheetViewModels dsvm in detailRecord)
            {
                if (dsvm.Direction == "借") balance += (dsvm.Debit - dsvm.Credit);

                if (dsvm.Direction == "贷") balance += (dsvm.Credit - dsvm.Debit);
                
                dsvm.Balance = balance;
            }
            detailSheet.AddRange(detailRecord);

            //本期合计
            string detailMonth = "select " +
                                    " max(v.VoucherDate) as VoucherDate," +
                                    " '' as CertWord," +
                                    " '本期合计' as Abstract," +
                                    " sum(vd.Debit) as Debit," +
                                    " sum(vd.Credit) as Credit," +
                                    " min(a.Direction) as Direction," +
                                    " max(v.PaymentTerms) as PaymentTerms," +
                                    " 0.00 as Balance " +
                                    " from T_Voucher v inner join T_Voucher_Detail vd on v.VId=vd.VId " +
                                    " left join T_Account a on a.AccId=vd.AccId " +
                                    string.Format(" where v.AbId={0} and vd.AccId={1} ", Utility.ParameterNameString("abid"), Utility.ParameterNameString("accid")) +
                                    string.Format(" and {0} <= v.PaymentTerms and v.PaymentTerms <= {1} ", Utility.ParameterNameString("startterm"), Utility.ParameterNameString("endterm")) +
                                    " group by v.VoucherYear,v.VoucherMonth"+
                                    " order by v.VoucherYear,v.VoucherMonth";

            var detailRecordM = _ledger.Database.SqlQuery<DetailSheetViewModels>(detailMonth, parames.Select(p => ((ICloneable)p).Clone()).ToArray()).ToList();
            balance = initDetail.Balance;
            foreach(DetailSheetViewModels dsvm in detailRecordM)
            {
                if (dsvm.Direction == "借") balance += (dsvm.Debit - dsvm.Credit);

                if (dsvm.Direction == "贷") balance += (dsvm.Credit - dsvm.Debit);

                dsvm.Balance = balance;
            }
            detailSheet.AddRange(detailRecordM);

            #endregion


            #region 本年累计
            string detailYear = "select " +
                                    " max(v.VoucherDate) as VoucherDate," +
                                    " '' as CertWord," +
                                    " '本年累计' as Abstract," +
                                    " sum(vd.Debit) as Debit," +
                                    " sum(vd.Credit) as Credit," +
                                    " min(a.Direction) as Direction," +
                                    " max(v.PaymentTerms) as PaymentTerms," +
                                    " 0.00 as Balance " +
                                    " from T_Voucher v inner join T_Voucher_Detail vd on v.VId=vd.VId " +
                                    " left join T_Account a on a.AccId=vd.AccId " +
                                    string.Format(" where v.AbId={0} and vd.AccId={1} ", Utility.ParameterNameString("abid"), Utility.ParameterNameString("accid")) +
                                    string.Format(" and v.PaymentTerms <= {0} ",  Utility.ParameterNameString("endterm")) +
                                    " group by v.VoucherYear" +
                                    " order by v.VoucherYear";

            List<DetailSheetViewModels> orderDetail = new List<DetailSheetViewModels>();
            foreach(string p in paymentTerms)
            {
                var detailRecordY = _ledger.Database.SqlQuery<DetailSheetViewModels>(detailYear,
                                                                                      Utility.NewParameter("abid", abid),
                                                                                      Utility.NewParameter("accid", condition.AccId),
                                                                                      Utility.NewParameter("endterm", p)).ToList();
                foreach(DetailSheetViewModels dsvm in detailRecordY)
                {
                    if (dsvm.Direction == "借") dsvm.Balance = initDetail.Balance + (dsvm.Debit - dsvm.Credit);

                    if (dsvm.Direction == "贷") dsvm.Balance = initDetail.Balance + (dsvm.Credit - dsvm.Debit);
                }
                detailSheet.AddRange(detailRecordY);

                //按会计期间排序(单个期间的顺序已排好只需按期间排列)
                orderDetail.AddRange(detailSheet.Where(d => d.PaymentTerms == p).ToList());
            }


            #endregion


            return orderDetail;
        }

        #endregion

        #region 总账

        private decimal CalBalance(GeneralLedgerViewModels genLedger)
        {
            decimal bal = 0;
            if(genLedger.Direction=="借")
            {
                bal += (genLedger.Debit - genLedger.Credit);
            }

            if (genLedger.Direction == "贷")
            {
                bal += (genLedger.Credit - genLedger.Debit);
            }

            return bal;
        }        

        public List<GeneralLedgerViewModels> GetGeneralLedger(SearchConditionViewModel condition)
        {
            string initBalance = "select "+
                                    "vd.AccId,"+
                                    "min(vd.AccountCode) as AccountCode,"+
                                    "min(vd.AccountName) as AccountName,"+
                                    string.Format("'{0}' as Period,", condition.StartPeriod) +
                                    "'期初余额' as Abstract,"+
                                    "SUM(vd.Debit) as Debit,"+
                                    "SUM(vd.Credit) as Credit,"+
                                    "min(a.Direction) as Direction,"+
                                    "0.00 as Balance,0 as MergeIndex,0 as RowSpan,1 as Sort " +
                                    "from T_Voucher v, T_Voucher_Detail vd, T_Account a "+
                                    "where v.VId = vd.VId and vd.AccId = a.AccId "+
                                    string.Format("and v.AbId = {0} ", Utility.ParameterNameString("abid")) +
                                    string.Format("and v.PaymentTerms < {0} ", Utility.ParameterNameString("startterm")) +
                                    "group by vd.AccId, v.VoucherYear";

            string curBalance = "select " +
                                    "vd.AccId," +
                                    "min(vd.AccountCode) as AccountCode," +
                                    "min(vd.AccountName) as AccountName," +
                                    "min(v.PaymentTerms) as Period," +
                                    "'本期合计' as Abstract," +
                                    "SUM(vd.Debit) as Debit," +
                                    "SUM(vd.Credit) as Credit," +
                                    "min(a.Direction) as Direction," +
                                    "0.00 as Balance,0 as MergeIndex,0 as RowSpan,2 as Sort " +
                                    "from T_Voucher v, T_Voucher_Detail vd, T_Account a " +
                                    "where v.VId = vd.VId and vd.AccId = a.AccId " +
                                    string.Format("and v.AbId = {0} ", Utility.ParameterNameString("abid")) +
                                    string.Format("and {0} <= v.PaymentTerms and v.PaymentTerms <= {1} ", Utility.ParameterNameString("startterm"), Utility.ParameterNameString("endterm")) +
                                    "group by vd.AccId, v.VoucherYear, v.VoucherMonth";

            string ytdBalance = "select " +
                                    "vd.AccId," +
                                    "min(vd.AccountCode) as AccountCode," +
                                    "min(vd.AccountName) as AccountName," +
                                    "max(v.PaymentTerms) as Period," +
                                    "'本年累计' as Abstract," +
                                    "SUM(vd.Debit) as Debit," +
                                    "SUM(vd.Credit) as Credit," +
                                    "min(a.Direction) as Direction," +
                                    "0.00 as Balance,0 as MergeIndex,0 as RowSpan,3 as Sort " +
                                    "from T_Voucher v, T_Voucher_Detail vd, T_Account a " +
                                    "where v.VId = vd.VId and vd.AccId = a.AccId " +
                                    string.Format("and v.AbId = {0} ", Utility.ParameterNameString("abid")) +
                                    string.Format("and v.PaymentTerms <= {0} ", Utility.ParameterNameString("endterm")) +
                                    "group by vd.AccId, v.VoucherYear";

            Guid abid = _cache.GetUserCache().AccountBookID;
            object[] parames = new object[] {
                Utility.NewParameter("abid", abid),
                Utility.NewParameter("startterm", condition.StartPeriod),
                Utility.NewParameter("endterm", condition.EndPeriod)
            };

            List<string> paymentTerms = _ledger.Database.SqlQuery<string>(string.Format("select PaymentTerms from T_Voucher where AbId = {0} and {1} <= PaymentTerms and PaymentTerms <= {2} group by PaymentTerms order by PaymentTerms", 
                                                                                         Utility.ParameterNameString("abid"), Utility.ParameterNameString("startterm"), Utility.ParameterNameString("endterm")),
                                                                          Utility.NewParameter("abid", abid), Utility.NewParameter("startterm", condition.StartPeriod), Utility.NewParameter("endterm", condition.EndPeriod)).ToList();

            List <GeneralLedgerViewModels> genLedger = _ledger.Database.SqlQuery<GeneralLedgerViewModels>(initBalance, parames).ToList();
            genLedger.AddRange(_ledger.Database.SqlQuery<GeneralLedgerViewModels>(curBalance, Utility.NewParameter("abid", abid), Utility.NewParameter("startterm", condition.StartPeriod), Utility.NewParameter("endterm", condition.EndPeriod)).ToList());

            foreach (string pt in paymentTerms)
            {
                genLedger.AddRange(_ledger.Database.SqlQuery<GeneralLedgerViewModels>(ytdBalance, Utility.NewParameter("abid", abid), Utility.NewParameter("endterm", pt)).ToList());
            }

            //记录排序
            genLedger = genLedger.OrderBy(gl => gl.AccId).ThenBy(gl => gl.Period).ThenBy(gl => gl.Sort).ToList();

            //设置合并列
            long accid = -1;
            for (int i=0;i<genLedger.Count;i++)
            {
                if (accid != genLedger[i].AccId)
                {
                    genLedger[i].MergeIndex = i;
                    genLedger[i].RowSpan = genLedger.Where(gl => gl.AccId == genLedger[i].AccId).Count();
                    accid = genLedger[i].AccId;
                }
            }

            return genLedger;
        }

        #endregion

        #region 科目余额表

        public List<AccountBalanceViewModels> GetAccountBalance(SearchConditionViewModel condition)
        {
            Guid abid = _cache.GetUserCache().AccountBookID;
            string initBalance = "select vd.AccId,vd.AccountCode,vd.AccountName,'init' as Period,a.Direction," +
                                 "(case  a.Direction when '借' then SUM(vd.Debit - vd.Credit) when '贷' then SUM(vd.Credit - vd.Debit) end) as Balance " +
                                 "from T_Voucher v, T_Voucher_Detail vd, T_Account a "+
                                 "where v.VId = vd.VId and vd.AccId = a.AccId "+
                                 string.Format("and v.AbId = {0} ", Utility.ParameterNameString("abid")) +
                                 string.Format("and v.VoucherYear = {0} and v.VoucherMonth < {1} ", Utility.ParameterNameString("year"), Utility.ParameterNameString("minmonth")) +
                                 "group by vd.AccId, vd.AccountCode, vd.AccountName, a.Direction ";

            string curBalance = "select vd.AccId,vd.AccountCode,vd.AccountName,'cur' as Period,a.Direction," +
                                "(case  a.Direction when '借' then SUM(vd.Debit - vd.Credit) when '贷' then SUM(vd.Credit - vd.Debit) end) as Balance "+
                                "from T_Voucher v, T_Voucher_Detail vd, T_Account a "+
                                "where v.VId = vd.VId and vd.AccId = a.AccId "+
                                string.Format("and v.AbId = {0} ", Utility.ParameterNameString("abid")) +
                                string.Format("and v.[VoucherYear] = {0} and v.VoucherMonth >= {1} and v.VoucherMonth <= {2} ", Utility.ParameterNameString("year"), Utility.ParameterNameString("minmonth"), Utility.ParameterNameString("maxmonth")) +
                                "group by vd.AccId, vd.AccountCode, vd.AccountName, a.Direction";

            string ytdBalance = "select vd.AccId,vd.AccountCode,vd.AccountName,'yearly' as Period,a.Direction," +
                                "(case  a.Direction when '借' then SUM(vd.Debit - vd.Credit) when '贷' then SUM(vd.Credit - vd.Debit) end) as Balance " +
                                "from T_Voucher v, T_Voucher_Detail vd, T_Account a " +
                                "where v.VId = vd.VId and vd.AccId = a.AccId " +
                                string.Format("and v.AbId = {0} ", Utility.ParameterNameString("abid")) +
                                string.Format("and v.[VoucherYear] = {0} and v.VoucherMonth <= {1} ", Utility.ParameterNameString("year"),  Utility.ParameterNameString("maxmonth")) +
                                "group by vd.AccId, vd.AccountCode, vd.AccountName, a.Direction";


            object[] parames = new object[] {
                Utility.NewParameter("abid", abid),
                Utility.NewParameter("year", condition.StartPeriod.Substring(0,4)),
                Utility.NewParameter("minmonth", condition.StartPeriod.Substring(4)),
                Utility.NewParameter("maxmonth", condition.EndPeriod.Substring(4))
            };

            List<AccountBalanceModel> accBalances = _ledger.Database.SqlQuery<AccountBalanceModel>(initBalance,parames).ToList();
            accBalances.AddRange(_ledger.Database.SqlQuery<AccountBalanceModel>(curBalance,parames.Select(p=>((ICloneable)p).Clone()).ToArray()).ToList());
            accBalances.AddRange(_ledger.Database.SqlQuery<AccountBalanceModel>(ytdBalance, parames.Select(p => ((ICloneable)p).Clone()).ToArray()).ToList());
            var accounts = _ledger.Accounts.Where(a=>a.AbId==abid).ToList();

            List<AccountBalanceViewModels> abViewModels = new List<AccountBalanceViewModels>();
            foreach(AccountBalanceModel ab in accBalances)
            {
                AccountBalanceViewModels abRow = abViewModels.Where(abvm => abvm.AccountCode == ab.AccountCode).FirstOrDefault();
                if(abRow == null)
                {
                    abRow = new AccountBalanceViewModels();
                    abRow.AccountCode = ab.AccountCode;
                    abRow.AccountName = ab.AccountName;
                    abRow.Direction = ab.Direction;

                    //科目期初余额
                    decimal initBal = accounts.Where(ai => ai.AccId == ab.AccId).Select(ai => ai.InitialBalance).FirstOrDefault();
                    if (ab.Direction == "借") abRow.InitDebit = initBal;
                    if (ab.Direction == "贷") abRow.InitCredit = initBal;

                    abViewModels.Add(abRow);
                }

                switch (ab.Period)
                {
                    //期初余额（加上科目期初）
                    case "init":
                        if (ab.Direction == "借") abRow.InitDebit += ab.Balance ;
                        if (ab.Direction == "贷") abRow.InitCredit += ab.Balance ;
                        break;
                    //本期发生额
                    case "cur":
                        if (ab.Direction == "借") abRow.CurOccurrenceDebit = ab.Balance;
                        if (ab.Direction == "贷") abRow.CurOccurrenceCredit = ab.Balance;
                        break;
                    //本年累计
                    case "yearly":
                        if (ab.Direction == "借") abRow.YtdDebit = ab.Balance;
                        if (ab.Direction == "贷") abRow.YtdCredit = ab.Balance;
                        break;
                }

            }

            //期末余额
            foreach(AccountBalanceViewModels abvm in abViewModels)
            {
                if (abvm.Direction == "借") abvm.DebitBalance = abvm.InitDebit + abvm.CurOccurrenceDebit;

                if (abvm.Direction == "贷") abvm.CreditBalance = abvm.CreditBalance + abvm.CurOccurrenceCredit;
            }

            return abViewModels;
        }

        #endregion

        #region 凭证汇总表

        public List<VoucherSummaryViewModels> GetVoucherSummary(SearchConditionViewModel condition)
        {
            return new List<VoucherSummaryViewModels>();
        }

        #endregion
    }

    public interface ILedgerSheet : IDependency
    {
        List<string> GetPaymentTerms();

        TreeViewModel<AccountViewModel> GetMyAccountsInVoucher();

        List<DetailSheetViewModels> GetDetailSheet(SearchConditionViewModel condition);

        List<GeneralLedgerViewModels> GetGeneralLedger(SearchConditionViewModel condition);

        List<AccountBalanceViewModels> GetAccountBalance(SearchConditionViewModel condition);
    }

    public class AccountBalanceModel
    {
        public long AccId { get; set; }

        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        public string Period { get; set; }

        public string Direction { get; set; }

        public decimal Balance { get; set; }
    }

    public class AccountInitBalanceModel
    {
        public long AccId { get; set; }

        public decimal InitialBalance { get; set; }
    }
}