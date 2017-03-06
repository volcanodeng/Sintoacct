using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Ledger.Models
{
    /// <summary>
    /// 行政区划
    /// </summary>
    [Table("T_Region")]
    public class Region
    {
        /// <summary>
        /// 行政区划编码
        /// </summary>
        [Key]
        public int RegionCode { get; set; }

        /// <summary>
        /// 省级区划编码
        /// </summary>
        public int ProvinceCode { get; set; }

        /// <summary>
        /// 市级区划编码
        /// </summary>
        public int CityCode { get; set; }

        /// <summary>
        /// 区划名称
        /// </summary>
        [MaxLength(50),Required]
        public string RegionName { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        [MaxLength(50),Required]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市级名称
        /// </summary>
        [MaxLength(50),Required]
        public string CityName { get; set; }

        /// <summary>
        /// 区划名称拼音
        /// </summary>
        [MaxLength(50)]
        public string Pinyin { get; set; }

        /// <summary>
        /// 区域内的公司
        /// </summary>
        public virtual ICollection<Company> Companies { get; set; }
    }
}
