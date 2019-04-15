using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using ReviewsCalculateSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.API
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
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
            var user = await db.Reviewers.Where(x => x.Name == context.UserName && x.Password == context.Password).FirstOrDefaultAsync();
            var admin = await db.Admins.Where(x => x.Name == context.UserName && x.Password == context.Password).FirstOrDefaultAsync();
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (admin != null && user == null)
            {
                var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                            "AdminId", Convert.ToString(admin.AdminId)
                        },
                        {
                             "Role" , "Admin"
                        }
                    });
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("UserName", admin.Name));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Admin Ahasanul Banna"));
                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            else if (user != null)
            {
                var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                            "ReviewerId", Convert.ToString(user.ReviewerId)
                        },
                        {
                             "Role" , "User"
                        }

                    });
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("UserName", user.Name));
                identity.AddClaim(new Claim(ClaimTypes.Name, "User Ahasanul Banna"));
                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            else
            {
                context.SetError("Invalid_grant", "Provided username & password is incorrect");
                return;
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}