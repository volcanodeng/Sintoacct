using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sintoacct.Ledger.Models
{
    public class DatagridViewModels<T> 
    {
        public int total { get; set; }

        public List<T> rows { get; set; }
    }
}