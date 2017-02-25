using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Sintoacct.Ledger.Models;
using IdentityManager.AspNetIdentity;

namespace Sintoacct.Ledger.IdentityManager
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext ctx)
            : base(ctx)
        {
        }
    }

    public class ApplicationRoleStore : RoleStore<IdentityRole>
    {
        public ApplicationRoleStore(ApplicationDbContext ctx)
            : base(ctx)
        {
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(ApplicationRoleStore roleStore)
            : base(roleStore)
        {
        }
    }

    public class ApplicationIdentityManagerService :
        AspNetIdentityManagerService<ApplicationUser, string, IdentityRole, string>
    {
        public ApplicationIdentityManagerService(ApplicationUserManager userMgr, ApplicationRoleManager roleMgr)
            : base(userMgr, roleMgr)
        {
        }
    }
}