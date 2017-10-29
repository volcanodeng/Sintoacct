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

        [Required]
        public string ServerImageName { get; set; }

        [ForeignKey("WorkProgress")]
        public long ProgId { get; set; }

        public WorkProgress WorkProgress { get; set; }
    }
}
