using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    [Table("T_Exception_Log")]
    public class ExceptionLog
    {
        [Key]
        public long LogId { get; set; }

        [MaxLength(500)]
        public string RequestUrl { get; set; }

        public string RequestDetail { get; set; }

        [MaxLength(200)]
        public string ExceptionMessage { get; set; }

        public string ExceptionDetail { get; set; }

        public DateTime LogTime { get; set; }
    }
}
