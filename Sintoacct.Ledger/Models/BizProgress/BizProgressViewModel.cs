using System;
using System.Collections.Generic;

namespace Sintoacct.Ledger.Models
{
    public class BizProgressViewModel
    {
        public long BizId { get; set; }

        public long CusId { get; set; }

        public string CustomerName { get; set; }

        public DateTime ContractTime { get; set; }

        public int CateId { get; set; }

        public string CategoryName { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int StepId { get; set; }

        public string StepName { get; set; }

        public string ProgressDesc { get; set; }

        public List<string> Images { get; set; }

        public string Remark { get; set; }

        public string BizManager { get; set; }

        public string BizOperations { get; set; }

        public string PromPerson { get; set; }

        public decimal? AmountReceivable { get; set; }

        public decimal? AmountReceived { get; set; }

        public decimal? AmountUnreceived
        {
            get
            {
                if (AmountReceivable.HasValue && AmountReceived.HasValue) return AmountReceivable.Value - AmountReceived.Value;
                else
                    return null;
            }
        }


        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}