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

    public class WorkProgressViewModel
    {
        public long ProgId { get; set; }

        public long WoId { get; set; }

        public int ItemId { get; set; }

        public int StepId { get; set; }

        public string StepName { get; set; }

        public DateTime? CompletedTime { get; set; }

        public string ResultDesc { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }

        public decimal AdvanceExpenditure { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class WorkProgressGetViewModel
    {
        public long WoId { get; set; }

        public int ItemId { get; set; }
    }

    public class BizPersonViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}