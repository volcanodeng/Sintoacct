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
    /// 用户账套。
    /// 记录每个用户可以操作的账套。账套与用户为多对多关系。
    /// </summary>
    [Table("T_User_Book")]
    public class UserBook
    {
        /// <summary>
        /// 用户编号。
        /// 对应Sintoacct.Ledger.Models.ApplicationUser的唯一编号。
        /// </summary>
        [Key,Column(Order =1)]
        public string UserId { get; set; }

        /// <summary>
        /// 账套编号
        /// </summary>
        [Key,Column(Order =2),ForeignKey("AccountBook")]
        public Guid AbId { get; set; }

        /// <summary>
        /// 账套
        /// </summary>
        public AccountBook AccountBook { get; set; }
    }
}
