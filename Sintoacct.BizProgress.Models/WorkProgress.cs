using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Progress.Models
{
    [Table("T_Prog_WorkProgress")]
    public class WorkProgress
    {
        /// <summary>
        /// 进度id
        /// </summary>
        [Key]
        public long ProgId { get; set; }

        /// <summary>
        /// 工单id
        /// </summary>
        [ForeignKey("WorkOrder")]
        public long WoId { get; set; }

        /// <summary>
        /// 关联工单
        /// </summary>
        public WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// 业务项目id
        /// </summary>
        [ForeignKey("BizItem")]
        public int ItemId { get; set; }

        /// <summary>
        /// 业务项目
        /// </summary>
        public BizItems BizItem { get; set; }

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
        /// 进度完成时间
        /// </summary>
        public DateTime? CompletedTime { get; set; }

        /// <summary>
        /// 办理结果
        /// </summary>
        [MaxLength(100)]
        public string ResultDesc { get; set; }

        /// <summary>
        /// 凭证图片。
        /// 图片文件保存到七牛，数据库仅保存访问地址
        /// </summary>
        public virtual ICollection<ProgressImage> Images { get; set; } 
    }
}
