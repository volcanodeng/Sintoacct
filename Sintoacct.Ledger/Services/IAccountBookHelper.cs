using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger
{
    public interface IAccountBookHelper : IDependency
    {

        List<AccountBook> GetBooksOfUser();

        AccountBook Save(AcctBookViewModels acctBook);
    }
}
