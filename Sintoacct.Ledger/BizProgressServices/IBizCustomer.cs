using System.Collections.Generic;
using Sintoacct.Ledger.Models;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public interface IBizCustomer : IDependency
    {
        Customers GetCustomer(long cusId);
        List<Customers> GetCustomers(BizCustomerConditionViewModel condition);
        List<Customers> GetCustomers();
        List<Customers> GetValidCustomers();
        BizPromotion GetPromotion(long promId);
        Customers SaveCustomer(BizCustomerViewModel customer);
        BizPromotion SavePromotion(BizPromotionViewModel promotion);
        void DeleteCustomer(long cusId);

    }
}