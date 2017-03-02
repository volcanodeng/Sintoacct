using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Reflection;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacConfig();
        }

        private void AutofacConfig()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            // MVC - Register your MVC controllers.
            builder.RegisterControllers(assembly);

            // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // MVC - OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            //注入实现Sintoacct.Ledger.IDependency接口的对象
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.GetInterface("Sintoacct.Ledger.IDependency") != null)
                   .AsImplementedInterfaces();
            //注入Ledger上下文
            var ledger = new LedgerContext();
            builder.RegisterInstance(ledger).As<LedgerContext>();

            // WebAPI - Register your Web API controllers
            builder.RegisterApiControllers(assembly);

            // WebAPI - OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);


            IContainer container = builder.Build();
            //注入MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //注入Web API
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
