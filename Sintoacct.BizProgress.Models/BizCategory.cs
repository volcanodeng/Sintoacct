using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.BizProgress.Models
{
    /// <summary>
    /// 业务大类
    /// </summary>
    [Table("T_Prog_BizCategory")]
    public class BizCategory
    {
        /// <summary>
        /// 大类id
        /// </summary>
        [Key]
        public int CateId { get; set; }

        /// <summary>
        /// 业务类别名称
        /// </summary>
        [MaxLength(50)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 类别排序
        /// </summary>
        public int SortIndex { get; set; }

            
        public virtual ICollection<BizSubCategory> SubCategorys { get; set; }

        public virtual ICollection<BizSteps> BizSteps { get; set; }

        public virtual ICollection<BizProgress> BizProgress { get; set; }
    }
}
