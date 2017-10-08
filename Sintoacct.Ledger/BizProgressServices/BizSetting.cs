using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public class BizSetting
    {
        private readonly BizProgressContext _context;

        public BizSetting(BizProgressContext progContext)
        {
            _context = progContext;
        }

        public List<BizCategory> GetBizCategories()
        {
            return _context.BizCategories.Include("BizItems").Include("BizSteps").OrderBy(c=>c.SortIndex).ToList();
        }
    }
}