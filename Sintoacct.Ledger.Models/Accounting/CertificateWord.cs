using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    /// <summary>
    /// 凭证字
    /// </summary>
    [Table("T_Certificate_Word")]
    public class CertificateWord
    {
        /// <summary>
        /// 凭证字ID
        /// </summary>
        [Key]
        public int CwId { get; set; }

        /// <summary>
        /// 凭证字
        /// </summary>
        [MaxLength(50)]
        public string CertWord { get; set; }

        /// <summary>
        /// 打印标题
        /// </summary>
        [MaxLength(50)]
        public string PrintTitle { get; set; }

        /// <summary>
        /// 是否默认凭证字。
        /// 每个账套只能有一个默认凭证字。
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        [ForeignKey("AccountBook")]
        public Guid AbId { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        public AccountBook AccountBook { get; set; }

    }
}
