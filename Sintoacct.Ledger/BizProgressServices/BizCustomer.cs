using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Progress.Models;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public class BizCustomer : IBizCustomer
    {
        private readonly BizProgressContext _context;

        public BizCustomer(BizProgressContext progContext)
        {
            _context = progContext;
        }

        public List<Customers> GetCustomers()
        {
            return _context.Customers.OrderByDescending(c => c.Level).ToList();
        }

        public Customers SaveCustomer(BizCustomerViewModel customer)
        {
            return null;
        }

        public BizPromotion SavePromotion(BizPromotionViewModel promotion)
        {
            return null;
        }
    }
}