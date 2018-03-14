using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public interface IReportService : IDependency
    {
        List<ProgressListViewModel> GetProgressList(ProgressSearchViewModel condition);

        List<string> GetProgressCreators();
    }
}