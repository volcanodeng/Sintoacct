using Microsoft.Owin;
using Owin;
using IdentityManager;
using IdentityManager.Configuration;
using Sintoacct.Ledger.IdentityManager;
using Sintoacct.Ledger.Models;

[assembly: OwinStartupAttribute(typeof(Sintoacct.Ledger.Startup))]
namespace Sintoacct.Ledger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.Map("/idm", idm =>
            {
                var factory = new IdentityManagerServiceFactory();
                factory.IdentityManagerService = new Registration<IIdentityManagerService, ApplicationIdentityManagerService>();
                factory.Register(new Registration<ApplicationUserManager>());
                factory.Register(new Registration<ApplicationUserStore>());
                factory.Register(new Registration<ApplicationRoleManager>());
                factory.Register(new Registration<ApplicationRoleStore>());
                factory.Register(new Registration<ApplicationDbContext>());

                idm.UseIdentityManager(new IdentityManagerOptions
                {
                    Factory = factory
                });
            });

        }
    }
}
