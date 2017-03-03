using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    /// <summary>
    /// 账套
    /// </summary>
    [Table("T_Account_Book")]
    public class AccountBook
    {
        /// <summary>
        /// 账套编号
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AbId { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [MaxLength(10)]
        public string Currency { get; set; }

        /// <summary>
        /// 启动时间（年度）
        /// </summary>
        public int StartYear { get; set; }

        /// <summary>
        /// 启动时间（账期）
        /// </summary>
        public int StartPeriod { get; set; }

        /// <summary>
        /// 会计制度
        /// </summary>
        public FiscalSystem FiscalSystem { get; set; }

        /// <summary>
        /// 账套对应的公司
        /// </summary>
        [ForeignKey("Company")]
        public int ComId { get; set; }

        /// <summary>
        /// 账套对应的公司
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 账套管理员
        /// </summary>
        public virtual ICollection<UserBook> Users { get; set; }

        /// <summary>
        /// 辅助核算类型
        /// </summary>
        public virtual ICollection<AuxiliaryType> AuxiliaryTypes { get; set; }

        /// <summary>
        /// 辅助核算
        /// </summary>
        public virtual ICollection<Auxiliary> Auxiliaries { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public virtual ICollection<Voucher> Vouchers { get; set; }

        /// <summary>
        /// 凭证模板
        /// </summary>
        public virtual ICollection<VoucherDetailTemplate> VoucherDetailTemplates { get; set; }

        /// <summary>
        /// 科目
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; }
    }

    public enum FiscalSystem
    {
        /// <summary>
        /// 小企业会计准则（2013年颁）
        /// </summary>
        小企业会计准则_2013年颁 = 1,
        /// <summary>
        /// 新会计准则（2006年颁）
        /// </summary>
        新会计准则_2006年颁 = 2
    }
}
