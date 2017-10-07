using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    /// <summary>
    /// 客户费用
    /// </summary>
    [Table("T_Prog_CustomerCost")]
    public class CustomerCost
    {
        /// <summary>
        /// 客户id
        /// </summary>
        [Key,Column(Order =1),ForeignKey("Customer")]
        public long CusId { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public Customers Customer { get; set; }

        /// <summary>
        /// 业务项目id
        /// </summary>
        [Key,Column(Order =2),ForeignKey("BizItem")]
        public int ItemId { get; set; }

        /// <summary>
        /// 业务项目
        /// </summary>
        public BizItems BizItem { get; set; }

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
        /// 收款日期
        /// </summary>
        [MaxLength(100)]
        public string CollectionDays { get; set; }

        /// <summary>
        /// 收款方式
        /// </summary>
        [MaxLength(10)]
        public string CreditingType { get; set; }

    }
}
