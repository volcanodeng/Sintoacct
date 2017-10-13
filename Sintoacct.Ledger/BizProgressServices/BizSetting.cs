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

        public BizCategory GetBizCategory(int cateId)
        {
            return _context.BizCategories.Include("BizItems").Include("BizSteps").Where(c => c.CateId == cateId).FirstOrDefault();
        }

        public List<BizItems> GetBizItems()
        {
            return _context.BizItems.OrderBy(i => i.SortIndex).ToList();
        }

        public BizItems GetBizItem(int itemId)
        {
            return _context.BizItems.Where(i => i.ItemId == itemId).FirstOrDefault();
        }

        public List<BizSteps> GetSteps()
        {
            return _context.BizSteps.OrderBy(s => s.SortIndex).ToList();
        }
        
        public BizSteps GetStep(int stepId)
        {
            return _context.BizSteps.Where(s => s.StepId == stepId).FirstOrDefault();
        }
    }


}