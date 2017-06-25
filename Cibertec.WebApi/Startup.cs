using Owin;
using System.Web.Http;
using Cibertec.UnitOfWork;

namespace Cibertec.WebApi
{
    public partial class Startup
    {
        private readonly IUnitOfWork _unit;

        public Startup()
        {
            _unit = new CibertecUnitOfWork();
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureInjector(app, config);
            Register(config);            
            app.UseWebApi(config);
        }
        
    }
}