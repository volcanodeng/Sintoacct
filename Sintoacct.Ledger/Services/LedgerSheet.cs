using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class LedgerSheet : ILedgerSheet
    {
        private readonly LedgerContext _ledger;
        private readonly ICacheHelper _cache;

        public LedgerSheet(LedgerContext ledger,
                           ICacheHelper cache)
        {
            _ledger = ledger;
            _cache = cache;
        }

        public List<string> GetPaymentTerms()
        {
            Guid abid = _cache.GetUserCache().AccountBookID;

            var terms = _ledger.Database.SqlQuery<string>("select [PaymentTerms] from [T_Voucher] where [AbId]=@abid group by [PaymentTerms]", new System.Data.SqlClient.SqlParameter("@abid", abid)).ToList();
            return terms;
        }

        public List<Account> GetMyAccountsInVoucher()
        {
            return new List<Account>();
        }
    }

    public interface ILedgerSheet : IDependency
    {
        List<string> GetPaymentTerms();
    }
}