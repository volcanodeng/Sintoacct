using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sintoacct.Ledger.Models
{
    

    public class BizCategoryViewModel
    {
        public int CateId { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

        public int SortIndex { get; set; }
    }

    public class BizItemViewModel
    {
        public int ItemId { get; set; }

        [MaxLength(50)]
        public string ItemName { get; set; }

        public int SortIndex { get; set; }
    }

    public class BizStepsViewModel
    {
        public int StepId { get; set; }

        [MaxLength(50)]
        public string StepName { get; set; }

        public int SortIndex { get; set; }
    }
}