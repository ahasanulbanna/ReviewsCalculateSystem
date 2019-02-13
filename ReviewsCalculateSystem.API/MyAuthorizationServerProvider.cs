using Microsoft.Owin.Security.OAuth;
using ReviewsCalculateSystem.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.API
{
    public class MyAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        private readonly ReviewDbContext db;
        public MyAuthorizationServerProvider()
        {
            db = new ReviewDbContext();
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {            
            context.Validated();          
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = db.Reviewers.Where(x => x.Name == context.UserName && x.Password == context.Password).FirstOrDefault();
            var admin = db.Admins.Where(x => x.Name == context.UserName && x.Password == context.Password).FirstOrDefault();

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if(admin!=null && user==null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("UserName", admin.Name));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Admin Ahasanul Banna"));
                context.Validated(identity);
            }
            else if (user!=null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("UserName", user.Name));
                identity.AddClaim(new Claim(ClaimTypes.Name, "User Ahasanul Banna"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Invalid_grant", "Provided username & password is incorrect");
                return;
            }
        }
    }
}