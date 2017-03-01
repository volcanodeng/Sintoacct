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


namespace Sintoacct.Ledger
{
    public class AccountBookHelper : IAccountBookHelper
    {
        private readonly IPrincipal _user;

        public AccountBookHelper(HttpContextBase context)
        {
            _user = context.User;
        }

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

    }
}
