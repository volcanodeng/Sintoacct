using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    [Table("T_Account")]
    public class Account
    {
        [Key]
        public long AccId { get; set; }

        [MaxLength(20),Required]
        public string AccCode { get; set; }

        [MaxLength(20)]
        public string ParentAccCode { get; set; }

        [ForeignKey("AccountCategory"),Required]
        public int AcId { get; set; }

        public AccountCategory AccountCategory { get; set; }
    }
}
