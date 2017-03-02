using System.Web.Mvc;
using System.Security.Claims;
using System.Web;
using System;

namespace Sintoacct.Ledger
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private string claimType;
        private string[] claimValues;
        public ClaimsAuthorizeAttribute(string type,params string[] values)
        {
            this.claimType = type;
            this.claimValues = values;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (IsAuthorize(filterContext.HttpContext))
            {
                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private  bool IsAuthorize(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool IsAuth = false;

            ClaimsPrincipal user = httpContext.User as ClaimsPrincipal;
            if (user == null || !user.Identity.IsAuthenticated) return IsAuth;

            foreach (string claim in claimValues)
            {
                if (user.HasClaim(claimType, claim)) {
                    IsAuth = true;
                    break;
                }
            }

            return IsAuth;
        }
    }
}