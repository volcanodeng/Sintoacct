using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
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
        /// 凭证账期年份（查询用）
        /// </summary>
        public int VoucherYear { get; set; }

        /// <summary>
        /// 凭证账期账期（查询用）
        /// </summary>
        public int VoucherMonth { get; set; }

        /// <summary>
        /// 账期（当前凭证所在月份）。
        /// 根据VoucherYear和VoucherMonth自动计算。
        /// 格式：201704
        /// </summary>
        [Required,MaxLength(20)]
        public string PaymentTerms { get; set; }

        /// <summary>
        /// 附加单据数量
        /// </summary>
        public int InvoiceCount { get; set; }

        /// <summary>
        /// 附加单据的文件路径
        /// </summary>
        [MaxLength(255)]
        public string InvoicePath { get; set; }

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
        [MaxLength(50)]
        public string Review { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ReviewTime { get; set; }

        /// <summary>
        /// 凭证明细
        /// </summary>
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }

        /// <summary>
        /// 原始凭证文件
        /// </summary>
        public virtual ICollection<SourceDocument> Invoices { get; set; }
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
