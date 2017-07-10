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
    }

    /// <summary>
    /// 总账列表
    /// </summary>
    public class GeneralLedgerViewModels
    {
        public long AccId { get; set; }

        public string AccCode { get; set; }

        public string AccName { get; set; }

        public string Period { get; set; }

        public string Abstract { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal YtdDebit { get; set; }

        public decimal YtdCredit { get; set; }

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
}