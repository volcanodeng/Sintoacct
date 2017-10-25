using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    [Table("T_Prog_WorkOrder")]
    public class WorkOrder
    {
        public WorkOrder()
        {
            this.WorkOrderItems = new List<WorkOrderItem>();
            this.WorkOrderPayments = new List<WorkOrderPayment>();
            this.WorkProgresses = new List<WorkProgress>();
        }

        /// <summary>
        /// 工作单号
        /// </summary>
        [Key]
        public long WoId { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        [ForeignKey("Customer")]
        public long CusId { get; set; }

        /// <summary>
        /// 客户信息
        /// </summary>
        public Customers Customer { get; set; }

        /// <summary>
        /// 签约时间
        /// </summary>
        public DateTime ContractTime { get; set; }

        /// <summary>
        /// 业务办理（业务项目）
        /// </summary>
        public virtual ICollection<WorkOrderItem> WorkOrderItems { get; set; }

        /// <summary>
        /// 备注（特殊要求）
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 业务主管
        /// </summary>
        [MaxLength(50)]
        public string BizManager { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        [MaxLength(100)]
        public string BizOperations { get; set; }

        /// <summary>
        /// 工单的推荐人（可以跟客户的推荐人不同）
        /// </summary>
        public string Recommend { get; set; }

        /// <summary>
        /// 业务办理费用（业务项目标准费用总和）
        /// </summary>
        public decimal CommercialExpense { get; set; }

        /// <summary>
        /// 优惠金额（针对当前工单的总优惠额度）
        /// </summary>
        public decimal PreferentialAmount { get; set; }

        /// <summary>
        /// 代垫费用总额（所有进度记录中代垫费用的合计）
        /// </summary>
        public decimal AdvanceExpenditure { get; set; }

        /// <summary>
        /// 已收总金额（累计总和）
        /// </summary>
        public decimal AmountReceived { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [MaxLength(50)]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkOrderState State { get; set; }

        /// <summary>
        /// 工单支付记录
        /// </summary>
        public virtual ICollection<WorkOrderPayment> WorkOrderPayments { get; set; }

        /// <summary>
        /// 工单对应的工作进度
        /// </summary>
        public virtual ICollection<WorkProgress> WorkProgresses { get; set; }
    }

    public enum WorkOrderState
    {
        New = 1,
        InProcess = 2,
        Completed = 3,
        Deleted = -1
    }
}
