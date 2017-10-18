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
            return _context.BizCategories.Include("BizItems").OrderBy(c=>c.SortIndex).ToList();
        }

        public BizCategory GetBizCategory(int cateId)
        {
            return _context.BizCategories.Include("BizItems").Include("BizSteps").Where(c => c.CateId == cateId).FirstOrDefault();
        }

        public List<BizItems> GetBizItems()
        {
            return _context.BizItems.Include("BizCategory").OrderBy(i => i.CateId).ThenBy(i => i.SortIndex).ToList();
        }

        public BizItems GetBizItem(int itemId)
        {
            return _context.BizItems.Include("BizCategory").Where(i => i.ItemId == itemId).FirstOrDefault();
        }

        public List<BizItems> GetBizItemsInCate(int cateId)
        {
            return _context.BizItems.Include("BizCategory").Where(i => i.CateId == cateId).OrderBy(i => i.CateId).ThenBy(i => i.SortIndex).ToList();
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