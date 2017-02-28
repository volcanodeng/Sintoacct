using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Models
{
    /// <summary>
    /// 辅助核算类型
    /// </summary>
    [Table("T_Auxiliary_Type")]
    public class AuxiliaryType
    {
        /// <summary>
        /// 核算类型编号
        /// </summary>
        [Key]
        public int AtId { get; set; }

        /// <summary>
        /// 核算类型名称
        /// </summary>
        [MaxLength(20)]
        public string AuxType { get; set; }

        /// <summary>
        /// 所属账套
        /// </summary>
        [ForeignKey("AccountBook")]
        public Nullable<Guid> AbId { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        public AccountBook AccountBook { get; set; }
    }
}
