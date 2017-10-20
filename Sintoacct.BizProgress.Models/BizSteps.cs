using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    [Table("T_Prog_BizSteps")]
    public class BizSteps
    {
        [Key]
        public int StepId { get; set; }

        [MaxLength(50)]
        public string StepName { get; set; }

        public int SortIndex { get; set; }

        [ForeignKey("BizItem")]
        public int ItemId { get; set; }

        public BizItems BizItem { get; set; }

        public virtual ICollection<WorkProgress> WorkProgresses { get; set; }
    }
}
