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
    /// 具体业务
    /// </summary>
    [Table("T_Prog_BizSubCategory")]
    public class BizSubCategory
    {
        /// <summary>
        /// 具体业务id
        /// </summary>
        [Key]
        public int SubCateId { get; set; }

        /// <summary>
        /// 具体业务名称
        /// </summary>
        [MaxLength(50)]
        public string SubCategoryName { get; set; }

        public int SortIndex { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal AmountReceivable { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal PreferentialAmount { get; set; }

        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal AmountReceived { get; set; }

        /// <summary>
        /// 收款日期[收款方式]（多个日期用分号间隔）
        /// </summary>
        [MaxLength(100)]
        public string CollectionDays { get; set; }

        [ForeignKey("BizCategory")]
        public int CateId { get; set; }

        public BizCategory BizCategory { get; set; }

        public virtual ICollection<BizProgress> BizProgress { get; set; }
    }
}
