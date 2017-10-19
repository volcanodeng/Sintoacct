using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class CompanyHelper : ICompanyHelper
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

        public List<BizPersonViewModel> GetBizPersons()
        {
            string sql = "select UserId," +
                         "(select [ClaimValue] from AspNetUserClaims uc1 where uc1.UserId=uc.UserId and [ClaimType]='name') as UserName " +
                         "from AspNetUserClaims uc where [ClaimType]='role' and [ClaimValue]='business' and " +
                         "not exists(select 1 from AspNetUserClaims uc2 where uc2.UserId=uc.UserId and [ClaimValue]='IdentityManagerAdministrator')";
            return _ledger.Database.SqlQuery<BizPersonViewModel>(sql).ToList();
        }

        public BizPersonViewModel GetBizPerson(Guid userid)
        {
            string sql = "select [id] as UserId,username as UserName " +
                         string.Format("from AspNetUsers where [Id]='{0}'", userid.ToString("D"));

            return _ledger.Database.SqlQuery<BizPersonViewModel>(sql).FirstOrDefault();

        }
    }

    public interface ICompanyHelper : IDependency
    {
        Company Edit(Company editCom);

        Company GetCompanyByName(string name);

        List<BizPersonViewModel> GetBizPersons();

        BizPersonViewModel GetBizPerson(Guid userid);
    }

    
}