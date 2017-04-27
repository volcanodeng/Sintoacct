using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    /// <summary>
    /// 原始凭证文件
    /// </summary>
    [Table("T_Source_Document")]
    public class SourceDocument
    {
        /// <summary>
        /// 文件唯一编号。
        /// 用于服务端文件名。
        /// </summary>
        [Key]
        public Guid FileId { get; set; }

        /// <summary>
        /// 原始文件名
        /// </summary>
        [Required,MaxLength(255)]
        public string SourceFileName { get; set; }

        /// <summary>
        /// 服务器上保存的文件全名（包含绝对路径）
        /// </summary>
        //[Required,MaxLength(255)]
        //public string FullFileName { get; set; }

        /// <summary>
        /// 服务器上的相对地址
        /// </summary>
        [Required,MaxLength(255)]
        public string RelateFileName { get; set; }

        /// <summary>
        /// 相对路径（服务器上的文件夹路径）
        /// </summary>
        [Required,MaxLength(255)]
        public string RelatePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 凭证id
        /// </summary>
        [ForeignKey("Voucher")]
        public long? VId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public Voucher Voucher { get; set; }
    }
}
