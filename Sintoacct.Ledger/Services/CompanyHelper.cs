using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class CompanyHelper 
    {
        private readonly LedgerContext _ledger;

        public CompanyHelper(LedgerContext ledger)
        {
            _ledger = ledger;
        }

        public Company Edit(Company editCom)
        {
            Company com = _ledger.Companys.Where(c => c.ComId == editCom.ComId).FirstOrDefault();
            com.ComName = editCom.ComName;
            com.ComShortName = editCom.ComShortName;
            com.ComAddress = editCom.ComAddress;
            com.LegalRepresentative = editCom.LegalRepresentative;
            com.Mobile = editCom.Mobile;
            com.Region = _ledger.Regions.Where(r => r.RegionCode == editCom.RegionCode).FirstOrDefault();

            if (_ledger.SaveChanges() > 0) return com;

            return null;
        }

        public Company GetCompanyByName(string name)
        {
            return _ledger.Companys.Where(c => c.ComName == name).FirstOrDefault();
        }
    }

    public interface ICompanyHelper : IDependency
    {
        Company Edit(Company editCom);

        Company GetCompanyByName(string name);
    }
}