using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    [Table("T_Prog_WorkOrderItem")]
    public class WorkOrderItem
    {
        [Key,Column(Order =1)]
        [ForeignKey("WorkOrder")]
        public long WoId { get; set; }
        public WorkOrder WorkOrder { get; set; }


        [Key, Column(Order = 2)]
        [ForeignKey("BizItem")]
        public int ItemId { get; set; }
        public BizItems BizItem { get; set; }
    }
}
