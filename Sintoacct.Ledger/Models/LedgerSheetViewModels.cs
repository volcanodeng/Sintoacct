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
        public string AccCode { get; set; }

        public string AccName { get; set; }

        public string Period { get; set; }

        public string Abstract { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public string Direction { get; set; }

        public decimal Balance { get; set; }
    }
}