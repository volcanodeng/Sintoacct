using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    [Table("T_Prog_Customers")]
    public class Customers
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        [Key]
        public long CusId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [MaxLength(50),Required]
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户住所
        /// </summary>
        [MaxLength(200),Required]
        public string CustomerAddress { get; set; }

        /// <summary>
        /// 实际经营地
        /// </summary>
        [MaxLength(200)]
        public string BusinessAddress { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(50),Required]
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20),Required]
        public string Phone { get; set; }

        /// <summary>
        /// 客户级别
        /// </summary>
        public CustomerLevel Level { get; set; }

        //外部推荐人不做系统维护，直接记录名称即可
        /// <summary>
        /// 外部推荐人ID
        /// </summary>
        //[MaxLength(50)]
        //public string PromId { get; set; }

        /// <summary>
        /// 外部推荐人名称。
        /// 外部推荐人不做系统维护，直接记录名称即可
        /// </summary>
        [MaxLength(150),Required]
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

    public enum CustomerLevel
    {
        /// <summary>
        /// 初级
        /// </summary>
        初级客户 = 1,
        /// <summary>
        /// 中级
        /// </summary>
        中级客户 = 2,
        /// <summary>
        /// 高级
        /// </summary>
        高级客户 = 3,
        /// <summary>
        /// VIP
        /// </summary>
        VIP = 4,
        /// <summary>
        /// 至尊
        /// </summary>
        至尊客户 = 5
    }
}
