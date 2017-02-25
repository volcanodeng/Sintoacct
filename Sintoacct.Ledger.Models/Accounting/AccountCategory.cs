using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    [Table("T_Account_Category")]
    public class AccountCategory
    {
        [Key]
        public int AcId { get; set; }

        public int ParentAcId { get; set; }

        [MaxLength(50),Required]
        public string CategoryName { get; set; }
    }
}
