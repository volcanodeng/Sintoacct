using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Models
{
    /// <summary>
    /// 凭证模板
    /// </summary>
    [Table("T_Voucher_Detail_Template")]
    public class VoucherDetailTemplate
    {
        /// <summary>
        /// 凭证模板ID
        /// </summary>
        [Key]
        public long VdtId { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [Required,MaxLength(200)]
        public string Abstract { get; set; }

        /// <summary>
        /// 科目代码
        /// </summary>
        [Required,MaxLength(100)]
        public string AccountCode { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        [Required,MaxLength(100)]
        public string AccountName { get; set; }

        /// <summary>
        /// 数量（辅助核算选择数量）
        /// </summary>
        public decimal? Quantity { get; set; }

        /// <summary>
        /// 单价（辅助核算选择数量）
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 借方金额
        /// </summary>
        public decimal? Debit { get; set; }

        /// <summary>
        /// 贷方金额
        /// </summary>
        public decimal? Credit { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        [ForeignKey("AccountBook")]
        public Guid AbId { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        public AccountBook AccountBook { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Required,MaxLength(50)]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
