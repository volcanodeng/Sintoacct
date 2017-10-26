using System;
using System.Collections.Generic;

namespace Sintoacct.Ledger.Models
{
    public class WorkOrderViewModel
    {
        public long WoId { get; set; }

        public long CusId { get; set; }

        public string CustomerName { get; set; }

        public DateTime ContractTime { get; set; }

        public string BizItemIds { get; set; }

        public string BizItemNames { get; set; }

        public string Remark { get; set; }

        public string BizManager { get; set; }

        public string BizOperations { get; set; }

        public string Recommend { get; set; }

        public decimal CommercialExpense { get; set; }

        public decimal PreferentialAmount { get; set; }

        public decimal AdvanceExpenditure { get; set; }

        public decimal AmountReceived { get; set; }

        public int State { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class WorkOrderDelViewModel
    {
        public long WoId { get; set; }
    }

    public class BizPersonViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}