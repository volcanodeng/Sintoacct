using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sintoacct.Ledger.Models
{
    /// <summary>
    /// 用户数据缓存。
    /// </summary>
    public class UserCacheModel
    {
        public UserCacheModel()
        {
            
        }

        public string UserID
        {
            get;set;
        }

        public string AccountBookID
        {
            get;set;
        }
    }

    /// <summary>
    /// 科目缓存。按账套所属科目保存。
    /// </summary>
    public class AccountCacheModel
    {
        public string AccountBookID
        {
            get; set;
        }

        public List<Account> Accounts { get; set; }
    }

   
}