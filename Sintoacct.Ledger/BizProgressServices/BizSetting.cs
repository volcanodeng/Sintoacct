using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using Sintoacct.Progress.Models;
using Sintoacct.Ledger.Models;

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

        public BizCategory SaveCategory(BizCategoryViewModel cate)
        {
            BizCategory bizCate = new BizCategory();
            if(cate.CateId>0)
            {
                bizCate = this.GetBizCategory(cate.CateId);
            }
            bizCate.CategoryName = cate.CategoryName;
            bizCate.SortIndex = cate.SortIndex;

            _context.BizCategories.AddOrUpdate(bizCate);
            _context.SaveChanges();

            return bizCate;
        }

        public void DeleteCategory(int cateId)
        {
            BizCategory cate = this.GetBizCategory(cateId);
            if (cate.BizItems.Count() > 0) throw new InvalidOperationException("该分类带有下级项目，不能删除");

            _context.BizCategories.Remove(cate);
            _context.SaveChanges();
        }

        public List<BizItems> GetBizItems()
        {
            return _context.BizItems.Include("BizCategory").OrderBy(i => i.CateId).ThenBy(i => i.SortIndex).ToList();
        }

        public BizItems GetBizItem(int itemId)
        {
            return _context.BizItems.Include("BizCategory").Include("BizSteps").Where(i => i.ItemId == itemId).FirstOrDefault();
        }

        public List<BizItems> GetBizItemsInCate(int cateId)
        {
            return _context.BizItems.Include("BizCategory").Where(i => i.CateId == cateId).OrderBy(i => i.CateId).ThenBy(i => i.SortIndex).ToList();
        }

        public BizItems SaveBizItem(BizItemViewModel item)
        {
            BizItems bizItem = new BizItems();
            if(item.ItemId>0)
            {
                bizItem = this.GetBizItem(item.ItemId);
            }
            bizItem.BizCategory = this.GetBizCategory(item.CateId);
            bizItem.ItemName = item.ItemName;
            bizItem.ServicePrice = item.ServicePrice;
            bizItem.SortIndex = item.SortIndex;

            _context.BizItems.AddOrUpdate(bizItem);
            _context.SaveChanges();

            return bizItem;
        }

        public void DeleteBizItem(int itemId)
        {
            BizItems item = this.GetBizItem(itemId);
            if (item.BizSteps.Count > 0) throw new InvalidOperationException("该项目带有下级步骤，不能删除");

            _context.BizItems.Remove(item);
            _context.SaveChanges();
        }

        public List<BizSteps> GetSteps()
        {
            return _context.BizSteps.OrderBy(s => s.SortIndex).ToList();
        }
        
        public BizSteps GetStep(int stepId)
        {
            return _context.BizSteps.Include("BizItem").Where(s => s.StepId == stepId).FirstOrDefault();
        }

        public List<BizSteps> GetBizStepInItem(int itemId)
        {
            return _context.BizSteps.Where(s => s.ItemId == itemId).ToList();
        }

        public BizSteps SaveBizStep(BizStepsViewModel step)
        {
            BizSteps bizStep = new BizSteps();
            if(step.StepId>0)
            {
                bizStep = this.GetStep(step.StepId);
            }

            bizStep.BizItem = this.GetBizItem(step.ItemId);
            bizStep.StepName = step.StepName;
            bizStep.SortIndex = step.SortIndex;

            _context.BizSteps.AddOrUpdate(bizStep);
            _context.SaveChanges();

            return bizStep;
        }

        public void DeleteBizStep(int stepId)
        {
            BizSteps step = this.GetStep(stepId);
            if (step == null) throw new ArgumentNullException("找不到步骤信息：" + stepId);

            _context.BizSteps.Remove(step);
            _context.SaveChanges();
        }
    }


}