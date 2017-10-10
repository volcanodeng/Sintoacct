﻿using System.Collections.Generic;
using Sintoacct.Progress.Models;
using Sintoacct.Ledger;

namespace Sintoacct.Ledger.BizProgressServices
{
    public interface IBizSetting : IDependency
    {
        List<BizCategory> GetBizCategories();
        List<BizItems> GetBizItems();
        List<BizSteps> GetSteps();
    }
}