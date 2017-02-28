using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Models
{
    /// <summary>
    /// 凭证主表
    /// </summary>
    [Table("T_Voucher")]
    public class Voucher
    {
        /// <summary>
        /// 凭证ID
        /// </summary>
        [Key]
        public long VId { get; set; }

        /// <summary>
        /// 凭证字
        /// </summary>
        public CertificateWord CertificateWord { get; set; }

        /// <summary>
        /// 凭证字流水号
        /// </summary>
        public int CertWordSN { get; set; }

        /// <summary>
        /// 凭证日期
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// 账期（当前凭证所在月份）。
        /// 根据凭证日期自动计算。
        /// </summary>
        [Required,MaxLength(20)]
        public string PaymentTerms { get; set; }

        /// <summary>
        /// 附加单据数量
        /// </summary>
        public int InvoiceCount { get; set; }

        /// <summary>
        /// 凭证状态
        /// </summary>
        public VoucherState State { get; set; }

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

        /// <summary>
        /// 审核人
        /// </summary>
        public string Review { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ReviewTime { get; set; }

        /// <summary>
        /// 凭证明细
        /// </summary>
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
    }

    public enum VoucherState
    {
        /// <summary>
        /// 待审
        /// </summary>
        PaddingAudit = 1,
        /// <summary>
        /// 已审。
        /// 弃审后会变成待审状态。
        /// </summary>
        Audited = 2,
        /// <summary>
        /// 作废（删除后会变为作废状态）
        /// </summary>
        Invalid = -1
    }
}
