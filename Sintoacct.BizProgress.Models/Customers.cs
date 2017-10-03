using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.BizProgress.Models
{
    [Table("T_Prog_Customers")]
    public class Customers
    {
        [Key]
        public long CusId { get; set; }

        [MaxLength(50)]
        public string CustomerName { get; set; }

        [MaxLength(200)]
        public string CustomerAddress { get; set; }

        [MaxLength(200)]
        public string BusinessAddress { get; set; }

        [MaxLength(50)]
        public string Contacts { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string WeixinNick { get; set; }

        public int Level { get; set; }

        [ForeignKey("Promotions")]
        public long PromId { get; set; }

        public BizPromotion Promotions { get; set; }

        public virtual ICollection<BizProgress> BizProgress { get; set; }

    }
}
