using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using Cibertec.UnitOfWork;
using System.Security.Claims;

namespace Cibertec.WebApi.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUnitOfWork _unit;
        public SimpleAuthorizationServerProvider(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _unit.Users.ValidateUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Usuario o password incorrecto.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}