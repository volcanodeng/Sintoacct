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

        private string CacheKey()
        {
            return string.Format("{0}_{1}",Constants.UserCache, ((ClaimsIdentity)_context.User.Identity).GetUserId());
        }

        public UserCacheModel SetUserCache(UserCacheModel userCache)
        {
            
            UserCacheModel userProfile = (UserCacheModel)_context.Cache.Get(CacheKey());
            if(userProfile == null)
            {
                userCache.UserID = ((ClaimsIdentity)_context.User.Identity).GetUserId();
                _context.Cache.Add(CacheKey(), userCache, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(Constants.UserCacheExpiration), CacheItemPriority.Default, null);
            }
            else
            {
                _context.Cache[CacheKey()] = userCache;
            }

            return userCache;
        }

        public UserCacheModel GetUserCache()
        {
            return (UserCacheModel)_context.Cache.Get(CacheKey()); 
        }
    }

    public interface ICacheHelper : IDependency
    {
        UserCacheModel SetUserCache(UserCacheModel userCache);

        UserCacheModel GetUserCache();
    }
}