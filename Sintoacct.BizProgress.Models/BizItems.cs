using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    /// <summary>
    /// 业务项目
    /// </summary>
    [Table("T_Prog_BizItems")]
    public class BizItems
    {
        /// <summary>
        /// 业务项目id
        /// </summary>
        [Key]
        public int ItemId { get; set; }

        /// <summary>
        /// 业务项目名称
        /// </summary>
        [MaxLength(50)]
        public string ItemName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortIndex { get; set; }

        [ForeignKey("BizCategory")]
        public int CateId { get; set; }

        public BizCategory BizCategory { get; set; }

        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
