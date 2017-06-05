﻿using System;
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
        private readonly LedgerContext _ledger;

        public CacheHelper(HttpContextBase context, LedgerContext ledger)
        {
            _context = context;
            _ledger = ledger;
        }

        private string UserCacheKey()
        {
            return string.Format("{0}_{1}",Constants.UserCache, ((ClaimsIdentity)_context.User.Identity).GetUserId());
        }

        private string AccountCacheKey(Guid acctBookId)
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
            UserCacheModel ucm = (UserCacheModel)_context.Cache.Get(UserCacheKey());
            if (ucm == null)
            {
                string userId = ((ClaimsIdentity)_context.User.Identity).GetUserId();
                Guid abid = _ledger.UserBooks.Where(ub => ub.UserId == userId && ub.AccountBook.State == AccountBookState.Normal).Select(ub => ub.AbId).FirstOrDefault();
                if(abid == Guid.Empty)
                {
                    throw new ArgumentNullException("当前用户没有创建账套");
                }
                ucm = new UserCacheModel();
                ucm.AccountBookID = abid;
                this.SetUserCache(ucm);
            }
            return ucm;
        }

        public void ClearUserCache()
        {
            _context.Cache.Remove(this.UserCacheKey());
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

        public AccountCacheModel GetAccountCache(Guid acctBookId)
        {
            return (AccountCacheModel)_context.Cache.Get(AccountCacheKey(acctBookId));
        }

        public void ClearAccountCache(Guid acctBookId)
        {
            _context.Cache.Remove(this.AccountCacheKey(acctBookId));
        }
    }

    public interface ICacheHelper : IDependency
    {
        UserCacheModel SetUserCache(UserCacheModel userCache);

        UserCacheModel GetUserCache();

        void ClearUserCache();

        AccountCacheModel SetAccountCache(AccountCacheModel acctCache);

        AccountCacheModel GetAccountCache(Guid acctBookId);

        void ClearAccountCache(Guid acctBookId);
    }
}