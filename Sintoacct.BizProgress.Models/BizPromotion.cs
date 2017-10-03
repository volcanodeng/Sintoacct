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
    /// 客户推广关系表
    /// </summary>
    [Table("T_Prog_BizPromotion")]
    public class BizPromotion
    {
        [Key]
        public long PromId { get; set; }

        public long ParentPromId { get; set; }

        /// <summary>
        /// 推广人员名称
        /// </summary>
        [MaxLength(50)]
        public string OpName { get; set; }

        /// <summary>
        /// 推广级别
        /// </summary>
        public int PromLevel { get; set; }

        /// <summary>
        /// 推广关系链
        /// </summary>
        [MaxLength(100)]
        public string PromChain { get; set; }
    }
}
