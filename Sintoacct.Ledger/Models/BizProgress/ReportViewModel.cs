using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sintoacct.Ledger.Models
{
    public class ProgressListViewModel
    {
        public string CustomerName
        {
            get; set;
        }

        public string ItemName
        {
            get;set;
        }

        public string StepName
        {
            get;set;
        }

        public string ResultDesc
        {
            get;set;
        }

        public DateTime? CompletedTime
        {
            get;set;
        }

        public string Creator
        {
            get;set;
        }

        public decimal CommercialExpense
        {
            get;set;
        }

        public DateTime ContractTime
        {
            get;set;
        }

        public string BizManager
        {
            get;set;
        }

        public string BizOperations
        {
            get;set;
        }

        public string Recommend
        {
            get;set;
        }

        public DateTime CreateTime
        {
            get;set;
        }

        public string Contacts
        {
            get;set;
        }

    }

    public class ProgressSearchViewModel
    {
        public DateTime? sCreate
        {
            get;set;
        }

        public DateTime? eCreate
        {
            get;set;
        }

        public string CustomerName
        {
            get;set;
        }

        public string Creator
        {
            get;set;
        }

        public string Contacts
        {
            get;set;
        }

    }
}