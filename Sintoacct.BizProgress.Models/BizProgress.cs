using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.BizProgress.Models
{
    [Table("T_Prog_BizProgress")]
    public class BizProgress
    {
        /// <summary>
        /// 业务单号
        /// </summary>
        [Key]
        public long BizId { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        [ForeignKey("Customer")]
        public long CusId { get; set; }

        /// <summary>
        /// 客户信息
        /// </summary>
        public Customers Customer { get; set; }

        /// <summary>
        /// 签约时间
        /// </summary>
        public DateTime ContractTime { get; set; }

        /// <summary>
        /// 大类id
        /// </summary>
        [ForeignKey("BizCategory")]
        public int CateId { get; set; }

        /// <summary>
        /// 业务大类
        /// </summary>
        public BizCategory BizCategory { get; set; }

        /// <summary>
        /// 小类id
        /// </summary>
        [ForeignKey("SubCategory")]
        public int SubCateId { get; set; }

        /// <summary>
        /// 业务小类
        /// </summary>
        public BizSubCategory SubCategory { get; set; }

        /// <summary>
        /// 业务步骤id
        /// </summary>
        [ForeignKey("BizStep")]
        public int StepId { get; set; }

        /// <summary>
        /// 业务步骤
        /// </summary>
        public BizSteps BizStep { get; set; }

        /// <summary>
        /// 进度描述
        /// </summary>
        [MaxLength(500)]
        public string ProgressDesc { get; set; }

        /// <summary>
        /// 进度凭证图片
        /// </summary>
        public virtual ICollection<ProgressImage> Images { get; set; }

        /// <summary>
        /// 备注（特殊要求）
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 业务主管
        /// </summary>
        [MaxLength(50)]
        public string BizManager { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        [MaxLength(100)]
        public string BizOperations { get; set; }
    }
}
