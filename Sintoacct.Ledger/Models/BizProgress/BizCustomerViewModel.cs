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

        public string Email { get; set; }

        public string WeixinNick { get; set; }

        public int Level { get; set; }

        public long PromId { get; set; }

        public string OpName { get; set; }
    }
}