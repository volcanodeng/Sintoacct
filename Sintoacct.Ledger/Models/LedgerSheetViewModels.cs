using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sintoacct.Ledger.Models
{
    /// <summary>
    /// 明细账列表
    /// </summary>
    public class DetailSheetViewModels
    {
        public DateTime VoucherDate { get; set; }

        public string CertWord { get; set; }

        public string Abstract { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public string Direction { get; set; }

        public decimal Balance { get; set; }

        public string PaymentTerms { get; set; }
    }

    /// <summary>
    /// 总账列表
    /// </summary>
    public class GeneralLedgerViewModels
    {
        public long AccId { get; set; }

        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        public string Period { get; set; }

        public string Abstract { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        //public decimal YtdDebit { get; set; }

        //public decimal YtdCredit { get; set; }

        public string Direction { get; set; }

        public decimal Balance { get; set; }

        /// <summary>
        /// 用于合并行的行索引
        /// </summary>
        public int MergeIndex { get; set; }

        /// <summary>
        /// 合并行的行数
        /// </summary>
        public int RowSpan { get; set; }

        /// <summary>
        /// 行排序专用，不显示在界面上
        /// </summary>
        public int Sort { get; set; }
    }

    /// <summary>
    /// 科目余额表
    /// </summary>
    public class AccountBalanceViewModels
    {
        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        public string Direction { get; set; }

        public decimal InitDebit { get; set; }

        public decimal InitCredit { get; set; }

        public decimal CurOccurrenceDebit { get; set; }

        public decimal CurOccurrenceCredit { get; set; }

        public decimal YtdDebit { get; set; }

        public decimal YtdCredit { get; set; }

        public decimal DebitBalance { get; set; }

        public decimal CreditBalance { get; set; }
    }

    /// <summary>
    /// 凭证汇总表
    /// </summary>
    public class VoucherSummaryViewModels
    {
        public long AccId { get; set; }

        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }
    }

    /// <summary>
    /// 多栏账
    /// </summary>
    public class MultiColumnInitViewModels
    {
        public string AccountsJson { get; set; }
    }

    public class MultiColumnViewModels
    {
        public MultiColumnViewModels()
        {
            SubAccountBalance = new List<BalanceOfSubAccount>();
        }

        public long VdId { get; set; }

        public int VoucherYear { get; set; }

        public int VoucherMonth { get; set; }

        public string CertWord { get; set; }

        public int CertWordSN { get; set; }

        public string Abstract { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public string Direction { get; set; }

        public decimal Balance { get; set; }

        public List<BalanceOfSubAccount> SubAccountBalance { get; set; }
    }

    public class BalanceOfSubAccount
    {
        public long VdId { get; set; }

        public long AccId { get; set; }

        public string AccountName { get; set; }

        public decimal Balance { get; set; }

    }
}




















