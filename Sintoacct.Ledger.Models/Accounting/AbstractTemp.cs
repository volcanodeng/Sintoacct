using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintoacct.Models
{
    /// <summary>
    /// 凭证摘要库
    /// </summary>
    [Table("T_Abstract_Temp")]
    public class AbstractTemp
    {
        /// <summary>
        /// 摘要编号
        /// </summary>
        [Key]
        public int AbsId { get; set; }

        /// <summary>
        /// 摘要内容
        /// </summary>
        [Required,MaxLength(200)]
        public string Abstract { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        [ForeignKey("AccountBook")]
        public Guid AbId { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        public AccountBook AccountBook { get; set; }
    }
}
