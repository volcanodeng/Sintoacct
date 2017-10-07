using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [ForeignKey("BizCategory")]
        public int CateId { get; set; }

        public BizCategory BizCategory { get; set; }

        public virtual ICollection<BizProgress> BizProgress { get; set; }
    }
}
