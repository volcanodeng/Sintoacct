using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    [Table("T_Prog_Customers")]
    public class Customers
    {
        [Key]
        public long CusId { get; set; }

        [MaxLength(50)]
        public string CustomerName { get; set; }

        [MaxLength(200)]
        public string CustomerAddress { get; set; }

        [MaxLength(200)]
        public string BusinessAddress { get; set; }

        [MaxLength(50)]
        public string Contacts { get; set; }

        [MaxLength(20),Required]
        public string Phone { get; set; }

        public int? Level { get; set; }

        [MaxLength(50)]
        public string PromId { get; set; }

        [MaxLength(50)]
        public string PromName { get; set; }

        public CustomerState State { get; set; }

        public virtual ICollection<WorkOrder> BizProgress { get; set; }

    }

    public enum CustomerState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 欠费停止服务
        /// </summary>
        Stopped = 0,
        /// <summary>
        /// 客户已注销
        /// </summary>
        Canceled = -1,
        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = -2
    }
}
