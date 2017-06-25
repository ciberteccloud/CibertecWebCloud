using Owin;
using System.Web.Http;
using Cibertec.UnitOfWork;

namespace Cibertec.WebApi
{
    public partial class Startup
    {        
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureInjector(app, config);
            ConfigureOAuth(app, config);
            Register(config);
            app.UseWebApi(config);
        }
        
    }
}