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
        private readonly HttpContextBase _context;

        public AuxiliaryHelper(LedgerContext ledger,ICacheHelper cache,IAccountBookHelper acctBook, HttpContextBase context)
        {
            _ledger = ledger;
            _cache = cache;
            _acctBook = acctBook;
            _context = context;
        }

        public List<AuxiliaryType> GetAuxiliaryType()
        {
            List<AuxiliaryType> baseAuxTypes = _ledger.AuxiliaryType.Where(at => !at.AbId.HasValue).ToList();
            UserCacheModel userCache = _cache.GetUserCache();
            if(userCache != null)
            {
                List<AuxiliaryType> customAuxType = _ledger.AuxiliaryType.Where(at => at.AbId.HasValue && at.AbId.Value == userCache.AccountBookID).ToList();
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

        public Auxiliary SaveAuxiliary(AuxiliaryViewModel vmAux)
        {
            Auxiliary aux = new Auxiliary();
            if(vmAux.AuxId>0)
            {
                aux = _ledger.Auxiliarys.Where(a => a.AuxId == vmAux.AuxId).FirstOrDefault();
                if (aux == null) return null;

                aux.AuxName = vmAux.AuxName;
            }
            else
            {
                aux.AuxCode = vmAux.AuxCode;
                aux.AuxName = vmAux.AuxName;
                aux.AuxiliaryState = AuxiliaryState.Normal;
                aux.AuxiliaryType = _ledger.AuxiliaryType.Where(at => at.AtId == vmAux.AtId).FirstOrDefault();
                aux.AccountBook = _acctBook.GetAccountBook(_cache.GetUserCache().AccountBookID);
                aux.Creator = _context.User.Identity.Name;
                aux.CreateTime = DateTime.Now;
                _ledger.Auxiliarys.Add(aux);
            }

            if (_ledger.SaveChanges() > 0) return aux;

            return null;
        }

        public Auxiliary DeleteAuxiliary(long auxid)
        {
            Auxiliary aux = _ledger.Auxiliarys.Where(a => a.AuxId == auxid).FirstOrDefault();
            aux = _ledger.Auxiliarys.Remove(aux);
            _ledger.SaveChanges();

            return aux;
        }

        public List<Auxiliary> GetAuxiliaryOfType(int auxTypeId)
        {
            return _ledger.Auxiliarys.Where(a => a.AtId == auxTypeId).ToList();
        }
    }

    public interface IAuxiliaryHelper : IDependency
    {
        List<AuxiliaryType> GetAuxiliaryType();

        AuxiliaryType Add(string typeName);

        AuxiliaryType Delete(int atid);

        Auxiliary SaveAuxiliary(AuxiliaryViewModel vmAux);

        Auxiliary DeleteAuxiliary(long auxid);

        List<Auxiliary> GetAuxiliaryOfType(int auxTypeId);
    }
}