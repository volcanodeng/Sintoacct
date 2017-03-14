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
        private readonly IAccountBookHelper _acctBook;

        public AuxiliaryHelper(LedgerContext ledger,ICacheHelper cache,IAccountBookHelper acctBook)
        {
            _ledger = ledger;
            _cache = cache;
            _acctBook = acctBook;
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

        public AuxiliaryType Add(string typeName)
        {
            AuxiliaryType auxType = new AuxiliaryType();
            auxType.AuxType = typeName;
            auxType.AccountBook = _acctBook.GetAccountBook(_cache.GetUserCache().AccountBookID);

            _ledger.AuxiliaryType.Add(auxType);
            _ledger.SaveChanges();

            return auxType;
        }

        public AuxiliaryType Delete(int atid)
        {
            AuxiliaryType auxType = _ledger.AuxiliaryType.Where(at => at.AtId == atid).FirstOrDefault();
            auxType = _ledger.AuxiliaryType.Remove(auxType);
            _ledger.SaveChanges();

            return auxType;
        }
    }

    public interface IAuxiliaryHelper : IDependency
    {
        List<AuxiliaryType> GetAuxiliaryType();

        AuxiliaryType Add(string typeName);

        AuxiliaryType Delete(int atid);
    }
}