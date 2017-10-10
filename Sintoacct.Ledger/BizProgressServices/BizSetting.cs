using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public class BizSetting : IBizSetting
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

        public List<BizItems> GetBizItems()
        {
            return _context.BizItems.OrderBy(i => i.SortIndex).ToList();
        }

        public List<BizSteps> GetSteps()
        {
            return _context.BizSteps.OrderBy(s => s.SortIndex).ToList();
        }
    }


}