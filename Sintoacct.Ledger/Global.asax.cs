using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Reflection;
using Sintoacct.Ledger.Models;
using System.Web.Http.ExceptionHandling;

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

            //Automapper配置
            AutoMapperConfiguration.Configure();

            //依赖注入
            AutofacConfig();

            //全局异常处理（异常日志）
            //GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger),new TraceExceptionHandle());

            //webapi序列化设置（默认是XML格式，现设置为Json）
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
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
            ledger.Database.Initialize(false);
            //#warning 输出SQL
            //ledger.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //builder.RegisterInstance(ledger).As<LedgerContext>();
            builder.RegisterType<LedgerContext>().InstancePerLifetimeScope();
            var common = new CommonContext();
            common.Database.Initialize(false);
            builder.RegisterType<CommonContext>().InstancePerLifetimeScope();

            // WebAPI - Register your Web API controllers
            builder.RegisterApiControllers(assembly);

            // WebAPI - OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            //注入Log4net
            builder.RegisterModule(new LogInjectionModule());


            IContainer container = builder.Build();
            //注入MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //注入Web API
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
