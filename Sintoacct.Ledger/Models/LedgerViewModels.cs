using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sintoacct.Ledger.Models
{
    public class LedgerViewModels
    {
        [Display(Name = "用户编号")]
        public string UserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "是否登录")]
        public bool IsAuth { get; set; }
    }
}