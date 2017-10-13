using System.Collections.Generic;
using Sintoacct.Ledger.Models;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public interface IBizProgressService : IDependency
    {
        BizProgress GetBizProgress(long bizId);
        BizProgress GetMyBizProgress(long bizId);
        List<BizProgress> GetMyBizProgresses();
        List<BizProgress> GetMyBizProgresses(int pageIndex, int pageSize);
        BizProgress SaveProgress(BizProgressViewModel bizProg);
    }
}