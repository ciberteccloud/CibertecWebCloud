using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using System.Web.Http;
using Thinktecture.IdentityModel.Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using Cibertec.UnitOfWork;

namespace Cibertec.WebApi
{
    public class Startup
    {
        private readonly IUnitOfWork _unit;

        public Startup()
        {
            _unit = new TiboxUnitOfWork();
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseBasicAuthentication(
                new BasicAuthenticationOptions("TiboxSecure",
                async (username, password)=> await Authenticate(username, password))
                );

            app.UseWebApi(config);
        }

        private async Task<IEnumerable<Claim>> Authenticate(string username, string password)
        {
            var user = _unit.Users.ValidateUser(username, password);

            if (user == null) return null;

            return new List<Claim>
            {
                new Claim("name", user.Email)
            };
        }
    }
}