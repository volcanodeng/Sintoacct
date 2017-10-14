using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    /// <summary>
    /// 工单付款记录
    /// </summary>
    [Table("T_Prog_WorkOrderPayment")]
    public class WorkOrderPayment
    {
        /// <summary>
        /// 付款id
        /// </summary>
        [Key,Column(Order =1)]
        public long PayId { get; set; }

        /// <summary>
        /// 工单id
        /// </summary>
        [ForeignKey("WorkOrder")]
        public long WoId { get; set; }

        /// <summary>
        /// 关联工单
        /// </summary>
        public WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal AmountReceived { get; set; }

        /// <summary>
        /// 收款日期
        /// </summary>
        [MaxLength(100)]
        public string CollectionDays { get; set; }

        /// <summary>
        /// 收款方式
        /// </summary>
        [MaxLength(10)]
        public string CreditingType { get; set; }

    }
}
