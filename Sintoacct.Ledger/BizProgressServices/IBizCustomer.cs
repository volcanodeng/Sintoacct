using System.Collections.Generic;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public interface IBizCustomer : IDependency
    {
        List<Customers> GetCustomers();
    }
}