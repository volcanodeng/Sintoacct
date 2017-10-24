using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sintoacct.Ledger.Models
{
    public class BizCustomerViewModel
    {
        public long CusId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string BusinessAddress { get; set; }

        public string Contacts { get; set; }

        public string Phone { get; set; }

        public int? Level { get; set; }

        public string PromId { get; set; }

        public string PromName { get; set; }

        public int State { get; set; }
    }

    public class BizCustomerConditionViewModel
    {
        public long? CusId { get; set; }

        public string CustomerName { get; set; }

        public string Phone { get; set; }
    }

    public class BizCustomerDelViewModel
    {
        public long CusId { get; set; }
    }

    public class BizPromotionViewModel
    {
        public long PromId { get; set; }

        public long? ParentPromId { get; set; }

        public string OpName { get; set; }

        public string WeixinOpenId { get; set; }

        public int PromLevel { get; set; }

        public string PromChain { get; set; }
    }
}