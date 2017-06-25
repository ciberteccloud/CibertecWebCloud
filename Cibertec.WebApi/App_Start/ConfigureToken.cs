using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using Cibertec.WebApi.Provider;
using Cibertec.UnitOfWork;
using SimpleInjector.Integration;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Cibertec.WebApi
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            var unit = config.DependencyResolver.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(unit)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}