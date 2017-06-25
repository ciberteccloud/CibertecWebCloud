using Owin;
using System.Web.Http;

namespace Cibertec.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            Register(config);            
            app.UseWebApi(config);
        }

       
    }
}