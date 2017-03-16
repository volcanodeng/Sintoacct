using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Sintoacct.Ledger.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Sintoacct.Ledger.Services
{
    public class CacheHelper : ICacheHelper
    {
        private readonly HttpContextBase _context;

        public CacheHelper(HttpContextBase context)
        {
            _context = context;
        }

        private string UserCacheKey()
        {
            return string.Format("{0}_{1}",Constants.UserCache, ((ClaimsIdentity)_context.User.Identity).GetUserId());
        }

        private string AccountCacheKey(string acctBookId)
        {
            return string.Format("{0}_{1}", Constants.AccountCache, acctBookId);
        }

        public UserCacheModel SetUserCache(UserCacheModel userCache)
        {
            
            UserCacheModel userProfile = (UserCacheModel)_context.Cache.Get(UserCacheKey());
            if(userProfile == null)
            {
                userCache.UserID = ((ClaimsIdentity)_context.User.Identity).GetUserId();
                _context.Cache.Add(UserCacheKey(), userCache, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(Constants.UserCacheExpiration), CacheItemPriority.Default, null);
            }
            else
            {
                _context.Cache[UserCacheKey()] = userCache;
            }

            return userCache;
        }

        public UserCacheModel GetUserCache()
        {
            return (UserCacheModel)_context.Cache.Get(UserCacheKey()); 
        }

        public AccountCacheModel SetAccountCache(AccountCacheModel acctCache)
        {
            if (acctCache == null) return null;

            string key = this.AccountCacheKey(acctCache.AccountBookID);
            AccountCacheModel acct = (AccountCacheModel)_context.Cache.Get(key);
            if(acct==null)
            {
                _context.Cache.Add(key, acctCache, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(Constants.AccountCacheExpiration), CacheItemPriority.Default, null);
            }
            else
            {
                _context.Cache[key] = acctCache;
            }

            return acctCache;
        }

        public AccountCacheModel GetAccountCache(string acctBookId)
        {
            return (AccountCacheModel)_context.Cache.Get(AccountCacheKey(acctBookId));
        }
    }

    public interface ICacheHelper : IDependency
    {
        UserCacheModel SetUserCache(UserCacheModel userCache);

        UserCacheModel GetUserCache();

        AccountCacheModel SetAccountCache(AccountCacheModel acctCache);

        AccountCacheModel GetAccountCache(string acctBookId);
    }
}