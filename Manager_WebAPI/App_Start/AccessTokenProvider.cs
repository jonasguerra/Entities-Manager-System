using System.Configuration;
using System.Security.Claims;
using Ftec.WebAPI.Infra.Repository;
using Microsoft.Owin.Security.OAuth;
using Manager_Application;
using Manager_Domain.Interfaces;
using System.Threading.Tasks;

namespace Manager_API
{
    public class AccessTokenProvider : OAuthAuthorizationServerProvider
    {
        private UserAplication userApplication;
        private IUserRepository userRepository;

        public AccessTokenProvider()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            userRepository = new UserRepository(connectionString);
            userApplication = new UserAplication(userRepository);
        }
        
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (userApplication.Autenticar(context.UserName, context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("Acesso Inválido", "Usuario ou senha são inválidos");
                return;
            }
        }
        
    }
}