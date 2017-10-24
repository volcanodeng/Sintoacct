using System.Collections.Generic;
using Sintoacct.Progress.Models;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public interface IBizSetting : IDependency
    {
        List<BizCategory> GetBizCategories();
        List<BizItems> GetBizItems();
        List<BizSteps> GetSteps();
        BizCategory GetBizCategory(int cateId);
        BizItems GetBizItem(int itemId);
        List<BizItems> GetBizItemsInCate(int cateId);
        BizSteps GetStep(int stepId);
        List<BizSteps> GetBizStepInItem(int itemId);

        BizCategory SaveCategory(BizCategoryViewModel cate);
        void DeleteCategory(int cateId);

        BizItems SaveBizItem(BizItemViewModel item);

        void DeleteBizItem(int itemId);

        BizSteps SaveBizStep(BizStepsViewModel step);

    }
}