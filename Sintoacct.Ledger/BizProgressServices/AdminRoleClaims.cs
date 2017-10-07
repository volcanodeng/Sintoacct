using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sintoacct.Ledger.BizProgressServices
{
    public static class AdminRoleClaims
    {
        private static string[] _admin = new string[] {"admin" };

        public static string[] Admins
        {
            get
            {
                return _admin;
            }
        }
    }
}