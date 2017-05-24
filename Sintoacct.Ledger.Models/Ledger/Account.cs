using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    /// <summary>
    /// 会计科目
    /// </summary>
    [Table("T_Account")]
    public class Account
    {
        /// <summary>
        /// 科目ID
        /// </summary>
        [Key]
        public long AccId { get; set; }

        /// <summary>
        /// 科目编号
        /// </summary>
        [MaxLength(20),Required]
        public string AccCode { get; set; }

        /// <summary>
        /// 父编码
        /// </summary>
        [MaxLength(20)]
        public string ParentAccCode { get; set; }

        /// <summary>
        /// 科目类别（取第二级类别）
        /// </summary>
        [ForeignKey("AccountCategory"),Required]
        public int AcId { get; set; }

        /// <summary>
        /// 科目类别（取第二级类别）
        /// </summary>
        public AccountCategory AccountCategory { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        [Required,MaxLength(50)]
        public string AccName { get; set; }

        /// <summary>
        /// 余额方向
        /// </summary>
        [Required,MaxLength(5)]
        public string Direction { get; set; }

        /// <summary>
        /// 是否启动辅助核算
        /// </summary>
        public bool IsAuxiliary { get; set; }

        /// <summary>
        /// 辅助核算类型id串
        /// </summary>
        [MaxLength(200)]
        public string AuxTypeIds { get; set; }

        /// <summary>
        /// 辅助核算名称串
        /// </summary>
        [MaxLength(200)]
        public string AuxTypeNames { get; set; }

        /// <summary>
        /// 是否启用数量核算
        /// </summary>
        public bool IsQuantity { get; set; }

        /// <summary>
        /// 数量核算的计量单位
        /// </summary>
        [MaxLength(5)]
        public string Unit { get; set; }

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

        /// <summary>
        /// 科目状态
        /// </summary>
        [Required]
        public AccountState State { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        [ForeignKey("AccountBook"),Required]
        public Guid AbId { get; set; }
        /// <summary>
        /// 账套
        /// </summary>
        public AccountBook AccountBook { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50),Required]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 凭证明细
        /// </summary>
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }

    }

    public enum AccountState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        Ban = 0
    }
}
