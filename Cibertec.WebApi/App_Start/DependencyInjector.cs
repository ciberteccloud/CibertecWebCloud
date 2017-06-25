using Cibertec.UnitOfWork;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;

namespace Cibertec.WebApi
{
    public partial class Startup
    {
        public void ConfigureInjector(IAppBuilder app, HttpConfiguration config)
        {
            var container = new Container();
            app.Use(async (context, next) =>
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });
            container.Options.DefaultScopedLifestyle = new  AsyncScopedLifestyle();
            RegisterAssemblies(container);
            container.RegisterWebApiControllers(config);
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private void RegisterAssemblies(Container container)
        {
            container.Register<IUnitOfWork, CibertecUnitOfWork>(Lifestyle.Transient);            
        }
    }
}