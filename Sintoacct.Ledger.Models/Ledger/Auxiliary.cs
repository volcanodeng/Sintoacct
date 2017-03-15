using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    [Table("T_Auxiliary")]
    public class Auxiliary
    {
        /// <summary>
        /// 辅助核算编号
        /// </summary>
        [Key]
        public long AuxId { get; set; }

        /// <summary>
        /// 辅助核算编码
        /// </summary>
        [MaxLength(20),Required]
        public string AuxCode { get; set; }

        /// <summary>
        /// 辅助核算名称
        /// </summary>
        [MaxLength(20),Required]
        public string AuxName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public AuxiliaryState AuxiliaryState { get; set; }

        /// <summary>
        /// 辅助核算类型ID
        /// </summary>
        [ForeignKey("AuxiliaryType")]
        public int AtId { get; set; }

        /// <summary>
        /// 辅助核算类型
        /// </summary>
        public AuxiliaryType AuxiliaryType { get; set; }

        /// <summary>
        /// 账套ID
        /// </summary>
        [ForeignKey("AccountBook")]
        public Guid AbId { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        public AccountBook AccountBook { get; set; }

        /// <summary>
        /// 创建人名称。userid在审计表记录。
        /// </summary>
        [MaxLength(50),Required]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 辅助核算状态
    /// </summary>
    public enum AuxiliaryState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal =1,
        /// <summary>
        /// 停用
        /// </summary>
        Stoped = 0
    }
}
