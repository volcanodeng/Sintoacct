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
            string sql = "select v.[CreateTime],cw.CertWord+'-'+CONVERT(nvarchar(10),v.certwordsn) as CertWord,vd.Abstract,vd.Debit,vd.Credit,a.Direction " +
                         "from T_Voucher v inner join T_Voucher_Detail vd on v.VId=vd.VId " +
                         "left join T_Certificate_Word cw on v.CertificateWord_CwId=cw.CwId " +
                         "left join T_Account a on a.AccId=vd.AccId " +
                         string.Format("where v.AbId={0} and vd.AccId={1} ", Utility.ParameterNameString("abid"), Utility.ParameterNameString("accid"))+
                         "order by vd.VdId";

            List<DetailSheetViewModels> sheets = _ledger.Database.SqlQuery<DetailSheetViewModels>(sql, Utility.NewParameter("abid", abid), Utility.NewParameter("accid", accid)).ToList();
            Account account = _ledger.Accounts.Where(a => a.AbId == abid && a.AccId == accid).FirstOrDefault();

            decimal balanceM = 0, balanceY = 0;
            string month = "";
            for(int i=0;i<sheets.Count;i++)
            {
                //判断是否本期
                if (month != string.Format("{0}-{1}", sheets[i].VoucherDate.Year, sheets[i].VoucherDate.Month))
                {
                    if (month != "")
                    {
                        //每期增加本期合计
                        DetailSheetViewModels monthSheet = new DetailSheetViewModels();
                        monthSheet.VoucherDate = new DateTime(sheets[i].VoucherDate.Year, sheets[i].VoucherDate.Month, DateTime.DaysInMonth(sheets[i].VoucherDate.Year, sheets[i].VoucherDate.Month));
                        monthSheet.Abstract = "本期合计";
                        monthSheet.Direction = sheets[i].Direction;
                        if (monthSheet.Direction == "借")
                        {
                            monthSheet.Debit = balanceM;
                        }
                        else
                        {
                            monthSheet.Credit = balanceM;
                        }
                        monthSheet.Balance = balanceM;

                        sheets.Insert(i, monthSheet);

                        balanceM = 0;
                    }

                    //修改本期标志
                    month = string.Format("{0}-{1}", sheets[i].VoucherDate.Year, sheets[i].VoucherDate.Month);
                }

                //累计金额
                balanceM += (sheets[i].Direction == "借" ? sheets[i].Debit- sheets[i].Credit : sheets[i].Credit- sheets[i].Debit);
                balanceY += (sheets[i].Direction == "借" ? sheets[i].Debit- sheets[i].Credit : sheets[i].Credit- sheets[i].Debit);
                sheets[i].Balance = balanceM;
            }

            //第一行加入期初余额
            DetailSheetViewModels initSheet = new DetailSheetViewModels();
            initSheet.VoucherDate = new DateTime(sheets[sheets.Count - 1].VoucherDate.Year, sheets[sheets.Count - 1].VoucherDate.Month, 1);
            initSheet.Abstract = "期初余额";
            initSheet.Direction = account.Direction;
            if (initSheet.Direction == "借")
            {
                initSheet.Debit = account.InitialBalance;
            }
            else
            {
                initSheet.Credit = account.InitialBalance;
            }
            initSheet.Balance = account.InitialBalance;
            sheets.Insert(0, initSheet);

            
            //每期增加本期合计
            DetailSheetViewModels monthSheetLast = new DetailSheetViewModels();
            monthSheetLast.VoucherDate = new DateTime(sheets[sheets.Count-1].VoucherDate.Year, sheets[sheets.Count - 1].VoucherDate.Month, DateTime.DaysInMonth(sheets[sheets.Count - 1].VoucherDate.Year, sheets[sheets.Count - 1].VoucherDate.Month));
            monthSheetLast.Abstract = "本期合计";
            monthSheetLast.Direction = sheets[sheets.Count - 1].Direction;
            if (monthSheetLast.Direction == "借")
            {
                monthSheetLast.Debit = balanceM;
            }
            else
            {
                monthSheetLast.Credit = balanceM;
            }
            monthSheetLast.Balance = balanceM;
            sheets.Add(monthSheetLast);

            //最后加入本年累计
            DetailSheetViewModels yearSheet = new DetailSheetViewModels();
            yearSheet.VoucherDate = new DateTime(sheets[sheets.Count - 1].VoucherDate.Year, sheets[sheets.Count - 1].VoucherDate.Month, DateTime.DaysInMonth(sheets[sheets.Count - 1].VoucherDate.Year, sheets[sheets.Count - 1].VoucherDate.Month));
            yearSheet.Abstract = "本年累计";
            yearSheet.Direction= sheets[sheets.Count - 1].Direction;
            if (yearSheet.Direction == "借")
            {
                yearSheet.Debit = balanceY;
            }
            else
            {
                yearSheet.Credit = balanceY;
            }
            yearSheet.Balance = balanceY;
            sheets.Add(yearSheet);

            return sheets;
        }

        #endregion

        #region 总账

        public List<GeneralLedgerViewModels> GetGeneralLedger(SearchConditionViewModel condition)
        {
            Guid abid = _cache.GetUserCache().AccountBookID;
            string sql = "select a.AccId,a.AccCode,a.AccName,v.PaymentTerms as Period,'本期合计' as Abstract," +
                         "SUM(vd.Debit) as Debit,SUM(vd.Credit) as Credit,max(vd.[YtdDebit]) as YtdDebit,max(vd.[YtdCredit]) as YtdCredit,min(a.Direction) as Direction,min(vd.[InitialBalance]) as Balance " +
                         "from T_Voucher v inner join T_Voucher_Detail vd on v.VId=vd.VId " +
                         "inner join T_Account a on vd.AccId=a.AccId " +
                         string.Format("where v.AbId={0} ", Utility.ParameterNameString("abid"))+
                         (Convert.ToInt64(condition.StartPeriod)<=Convert.ToInt64( condition.EndPeriod)?
                            string.Format(" and (v.[PaymentTerms] BETWEEN {0} and {1}) ", Utility.ParameterNameString("start"), Utility.ParameterNameString("end")) : 
                            string.Format(" and (v.[PaymentTerms] BETWEEN {1} and {0}) ", Utility.ParameterNameString("start"), Utility.ParameterNameString("end"))) +
#warning 查询条件
                         "group by a.AccId,a.AccCode,a.AccName,v.PaymentTerms " +
                         "order by a.AccCode,v.PaymentTerms";

            //string accSql = "select  vd.AccId from  T_Voucher v inner join T_Voucher_Detail vd on v.VId=vd.VId " +
            //                string.Format("where v.AbId={0} group by vd.AccId ", Utility.ParameterNameString("abid"));

            List<GeneralLedgerViewModels> genLedger = _ledger.Database.SqlQuery<GeneralLedgerViewModels>(sql, Utility.NewParameter("abid", abid), Utility.NewParameter("start", condition.StartPeriod), Utility.NewParameter("end", condition.EndPeriod)).ToList();
            //List<long> accids = _ledger.Database.SqlQuery<long>(accSql, Utility.NewParameter("abid", abid)).ToList();

            for(int i = 0;i<genLedger.Count;i++)
            {
                if(genLedger[i].Period==condition.StartPeriod)
                {
                    GeneralLedgerViewModels glInitBalance = new GeneralLedgerViewModels();
                    glInitBalance.AccId = genLedger[i].AccId;
                    glInitBalance.AccCode = genLedger[i].AccCode;
                    glInitBalance.AccName = genLedger[i].AccName;
                    glInitBalance.Period = genLedger[i].Period;
                    glInitBalance.Abstract = "期初余额";
                    glInitBalance.Debit = 0;
                    glInitBalance.Credit = 0;
                    glInitBalance.Balance = genLedger[i].Balance;
                    glInitBalance.Direction = (glInitBalance.Balance == 0 ? "平" : genLedger[i].Direction);
                    glInitBalance.Sort = 1;
                    genLedger.Add(glInitBalance);

                    genLedger[i].Sort = 2;

                    GeneralLedgerViewModels ytdBalance = new GeneralLedgerViewModels();
                    ytdBalance.AccId = genLedger[i].AccId;
                    ytdBalance.AccCode = genLedger[i].AccCode;
                    ytdBalance.AccName = genLedger[i].AccName;
                }
                else
                {

                }
            }

            return genLedger;
        }

        #endregion
    }

    public interface ILedgerSheet : IDependency
    {
        List<string> GetPaymentTerms();

        TreeViewModel<AccountViewModel> GetMyAccountsInVoucher();

        List<DetailSheetViewModels> GetDetailSheet(long accid);

        List<GeneralLedgerViewModels> GetGeneralLedger(SearchConditionViewModel condition);
    }
}