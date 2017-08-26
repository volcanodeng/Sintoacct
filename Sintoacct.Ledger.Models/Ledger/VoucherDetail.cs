using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    [Table("T_Voucher_Detail")]
    public class VoucherDetail
    {
        /// <summary>
        /// 凭证明细ID
        /// </summary>
        [Key]
        public long VdId { get; set; }

        /// <summary>
        /// 凭证主记录
        /// </summary>
        [ForeignKey("Voucher"),Required]
        public long VId { get; set; }

        /// <summary>
        /// 凭证主记录
        /// </summary>
        public Voucher Voucher { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [Required,MaxLength(200)]
        public string Abstract { get; set; }

        /// <summary>
        /// 会计科目id
        /// </summary>
        [ForeignKey("Account"),Required(ErrorMessage = "凭证科目")]
        public long AccId { get; set; }

        /// <summary>
        /// 会计科目
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// 科目代码（可生成扩展科目代码）
        /// </summary>
        [MaxLength(100)]
        public string AccountCode { get; set; }

        /// <summary>
        /// 科目名称（可生成扩展科目名称）
        /// </summary>
        [MaxLength(100)]
        public string AccountName { get; set; }

        /// <summary>
        /// 数量（辅助核算选择数量）
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 单价（辅助核算选择数量）
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 借方金额
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// 贷方金额
        /// </summary>
        public decimal Credit { get; set; }

        #region 统计字段

        /// <summary>
        /// 期初余额数量
        /// </summary>
        public decimal InitialQuantity { get; set; }

        /// <summary>
        /// 期初余额
        /// </summary>
        public decimal InitialBalance { get; set; }

        /// <summary>
        /// 本年累积借方数量
        /// </summary>
        public decimal YtdDebitQuantity { get; set; }

        /// <summary>
        /// 本年累积借方金额
        /// </summary>
        public decimal YtdDebit { get; set; }

        /// <summary>
        /// 本年累积贷方数量
        /// </summary>
        public decimal YtdCreditQuantity { get; set; }

        /// <summary>
        /// 本年累积贷方金额
        /// </summary>
        public decimal YtdCredit { get; set; }


        /// <summary>
        /// 年初余额数量
        /// </summary>
        public decimal YtdBeginBalanceQuantity { get; set; }

        /// <summary>
        /// 年初余额
        /// </summary>
        public decimal YtdBeginBalance { get; set; }

        #endregion
    }
}
