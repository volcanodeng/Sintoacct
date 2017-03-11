using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class CacheHelper
    {
        private readonly HttpContextBase _context;

        public CacheHelper(HttpContextBase context)
        {
            _context = context;
        }

        public void AddEditingAccountBook(string userId,string acctBookId)
        {
            AccountBookCacheModel accountBookCache = (AccountBookCacheModel)_context.Cache.Get(Constants.AccountBookCache);
            if(accountBookCache == null)
            {
                accountBookCache = new AccountBookCacheModel();
                _context.Cache.Add(Constants.AccountBookCache,accountBookCache,null,Cache.NoAbsoluteExpiration,TimeSpan.f)
            }

            if(accountBookCache.CurrentEditAccountBook.ContainsKey(userId))
            {
                accountBookCache.CurrentEditAccountBook[userId] = acctBookId;
            }
            else
            {
                accountBookCache.CurrentEditAccountBook.Add(userId, acctBookId);
            }



        }
    }
}