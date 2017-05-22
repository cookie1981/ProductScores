using System;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DatabaseProviders;
using api.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace api.Providers
{
    public class CustomApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IDatabaseRepository<User> _userRepository;

        public CustomApplicationOAuthProvider(IDatabaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var userName = context.Parameters["userName"];
            var password = context.Parameters["password"];

            var userMatch = await _userRepository.FindByUniqueElementAsync("userName", userName).ConfigureAwait(false);

            if (userMatch != null && userMatch.Password == password)
            {
                context.Validated();
            }
            await base.ValidateClientAuthentication(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = new ApplicationUser {UserName = context.UserName};

            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            try
            {
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
                context.Validated(ticket);
            }
            catch (Exception x)
            {
                var t = x;
            }
            return base.GrantResourceOwnerCredentials(context);
        }
    }
}