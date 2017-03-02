using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Sintoacct.Ledger.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Data.Entity;


namespace Sintoacct.Ledger
{
    public class AccountBookHelper : IAccountBookHelper
    {
        private ClaimsIdentity _identity;
        private readonly IPrincipal _user;
        private readonly LedgerContext _ledger;

        public AccountBookHelper(HttpContextBase context,LedgerContext ledger)
        {
            _user = context.User;
            _identity = _user.Identity as ClaimsIdentity;
            _ledger = ledger;
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public LedgerViewModels GetLedger()
        {
            ClaimsIdentity ci = _user.Identity as ClaimsIdentity;

            LedgerViewModels ledger = new LedgerViewModels()
            {
                UserId = ci.GetUserId(),
                UserName = ci.GetUserName(),
                IsAuth = ci.IsAuthenticated
            };

            return ledger;
        }

        public List<AccountBook> GetBooksOfUser()
        {
            string userId = _identity.GetUserId();
            List<UserBook> books = _ledger.UserBooks.Where(ub => ub.UserId == userId).Include(ub => ub.AccountBook).ToList();

            return books.Select(ub => ub.AccountBook).ToList();
        }

    }
}
