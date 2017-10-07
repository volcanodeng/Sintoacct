using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    [Table("T_Prog_ProgressImage")]
    public class ProgressImage
    {
        [Key]
        public long ImgId { get; set; }

        public string OriginalImageName { get; set; }

        public string ServerImageName { get; set; }

        [ForeignKey("BizProgress")]
        public long BizId { get; set; }

        public BizProgress BizProgress { get; set; }
    }
}
