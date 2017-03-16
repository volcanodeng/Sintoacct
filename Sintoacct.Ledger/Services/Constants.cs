using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sintoacct.Ledger.Services
{
    public static class Constants
    {
        public const string ClaimAccountBookID = "AccountBook_ID";

        /// <summary>
        /// 用户缓存KEY
        /// </summary>
        public const string UserCache = "User_Cache";

        /// <summary>
        /// 科目缓存KEY
        /// </summary>
        public const string AccountCache = "Account_Cache";

        /// <summary>
        /// 数据缓存过期时间[小时]（线性过期）
        /// </summary>
        public const int UserCacheExpiration = 6;

        /// <summary>
        /// 科目缓存过期时间（分钟）。
        /// </summary>
        public const int AccountCacheExpiration = 10;
    }
}