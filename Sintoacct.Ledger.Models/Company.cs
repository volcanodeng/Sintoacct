using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Models
{
    /// <summary>
    /// 公司信息
    /// </summary>
    [Table("T_Company")]
    public class Company
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        [Key]
        public int ComId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [MaxLength(50),Required]
        public string ComName { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        [MaxLength(50)]
        public string ComShortName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        [MaxLength(100)]
        public string ComAddress { get; set; }

        /// <summary>
        /// 公司法人姓名
        /// </summary>
        [MaxLength(10)]
        public string LegalRepresentative { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(30)]
        public string Mobile { get; set; }

        /// <summary>
        /// 公司所在地编码
        /// </summary>
        [ForeignKey("Region"),Required]
        public int RegionCode { get; set; }
        /// <summary>
        /// 公司所在地
        /// </summary>
        public Region Region { get; set; }
    }
}
