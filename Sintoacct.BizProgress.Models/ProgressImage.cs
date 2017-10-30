using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sintoacct.Progress.Models
{
    [Table("T_Prog_ProgressImage")]
    public class ProgressImage
    {
        [Key]
        public long ImgId { get; set; }

        [Required,MaxLength(64)]
        public string AliyunBucket { get; set; }

        [Required,MaxLength(300)]
        public string AliyunKey { get; set; }

        [Required,MaxLength(300)]
        public string Url { get; set; }

        /// <summary>
        /// 访问地址授权过期时间。绝对时间，获取Url前要判断是否过期，若已过期则必须重新生成Url。
        /// </summary>
        public DateTime Expiration { get; set; }

        [ForeignKey("WorkProgress")]
        public long ProgId { get; set; }

        public WorkProgress WorkProgress { get; set; }
    }
}
