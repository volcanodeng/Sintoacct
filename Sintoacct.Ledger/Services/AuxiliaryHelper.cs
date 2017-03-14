using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class AuxiliaryHelper : IAuxiliaryHelper
    {
        private readonly LedgerContext _ledger;
        private readonly ICacheHelper _cache;

        public AuxiliaryHelper(LedgerContext ledger,ICacheHelper cache)
        {
            _ledger = ledger;
            _cache = cache;
        }

        public List<AuxiliaryType> GetAuxiliaryType()
        {
            List<AuxiliaryType> baseAuxTypes = _ledger.AuxiliaryType.Where(at => !at.AbId.HasValue).ToList();
            UserCacheModel userCache = _cache.GetUserCache();
            if(userCache != null)
            {
                List<AuxiliaryType> customAuxType = _ledger.AuxiliaryType.Where(at => at.AbId.HasValue && at.AbId.Value.ToString() == userCache.AccountBookID).ToList();
                baseAuxTypes.AddRange(customAuxType);
            }

            return baseAuxTypes;
        }
    }

    public interface IAuxiliaryHelper : IDependency
    {
        List<AuxiliaryType> GetAuxiliaryType();
    }
}