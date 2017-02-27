using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.IO;

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
            //var assembly = Assembly.GetExecutingAssembly();

            var assemblies = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/bin/")).GetFiles("Sintoacct.*.dll")
                .Select(r => Assembly.LoadFrom(r.FullName)).ToArray();


            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => t.GetInterface("Sintoacct.Common.IDependency") != null)
                   .AsImplementedInterfaces();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
