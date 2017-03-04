using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sintoacct.Ledger.Models
{
    public class ResMessage
    {
        public ResMessage()
        {
            IsSuccess = true;

            Code = 1;

            Message = "成功";
        }

        public ResMessage(string Error)
        {
            IsSuccess = false;
            Code = 9;
            Message = Error;
        }

        public bool IsSuccess { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }
    }

    public class AcctBookListViewModels
    {
        public string AbId { get; set; }

        public string Currency { get; set; }

        public int StartYear { get; set; }

        public int StartPeriod { get; set; }

        public string FiscalSystem { get; set; }

        public string CompanyName { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class AcctBookViewModels
    {
        public string AbId { get; set; }

        [Required,MaxLength(50)]
        public string ComapnyName { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int StartPeriod { get; set; }

        [Required]
        public int FiscalSystem { get; set; }
    }

   
}