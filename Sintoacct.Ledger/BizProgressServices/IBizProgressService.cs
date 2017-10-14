using System.Collections.Generic;
using Sintoacct.Ledger.Models;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public interface IBizProgressService : IDependency
    {
        WorkOrder GetBizProgress(long bizId);
        WorkOrder GetMyBizProgress(long bizId);
        List<WorkOrder> GetMyBizProgresses();
        List<WorkOrder> GetMyBizProgresses(int pageIndex, int pageSize);
        WorkOrder SaveProgress(BizProgressViewModel bizProg);
    }
}