using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.ServiceModel.Security.Tokens;

namespace ReviewsCalculateSystem.Services
{
    public static class TokenManager
    {

        public static string CreateJwtToken(string userName, string role)
        {
            var claimList = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)     //Not sure what this is for
            };

            var tokenHandler = new JwtSecurityTokenHandler() { RequireExpirationTime = true };
            var sSKey = new InMemorySymmetricSecurityKey(SecurityConstants.KeyForHmacSha256);

            var jwtToken = tokenHandler.CreateToken(
                makeSecurityTokenDescriptor(sSKey, claimList));
            return tokenHandler.WriteToken(jwtToken);
        }

        public static ClaimsPrincipal ValidateJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler() { RequireExpirationTime = true };

            // Parse JWT from the Base64UrlEncoded wire form 
            //(<Base64UrlEncoded header>.<Base64UrlEncoded body>.<signature>)
            JwtSecurityToken parsedJwt = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            System.IdentityModel.Tokens.TokenValidationParameters validationParams =
                new System.IdentityModel.Tokens.TokenValidationParameters()
                {
                    AllowedAudience = SecurityConstants.TokenAudience,
                    ValidIssuer = SecurityConstants.TokenIssuer,
                    ValidateIssuer = true,
                    SigningToken = new BinarySecretSecurityToken(SecurityConstants.KeyForHmacSha256),
                };

            return tokenHandler.ValidateToken(parsedJwt, validationParams);
        }

        private static System.IdentityModel.Tokens.SecurityTokenDescriptor makeSecurityTokenDescriptor(
            InMemorySymmetricSecurityKey sSKey, List<Claim> claimList)
        {
            var now = DateTime.UtcNow;
            Claim[] claims = claimList.ToArray();
            return new System.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                TokenIssuerName = SecurityConstants.TokenIssuer,
                AppliesToAddress = SecurityConstants.TokenAudience,
                Lifetime = new Lifetime(now, now.AddMinutes(SecurityConstants.TokenLifetimeMinutes)),
                SigningCredentials = new System.IdentityModel.Tokens.SigningCredentials(sSKey,
                    "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                    "http://www.w3.org/2001/04/xmlenc#sha256"),
            };
        }
    }

    public class SecurityConstants
    {
        public static readonly byte[] KeyForHmacSha256 = new byte[64];

        public static readonly string TokenIssuer = string.Empty;

        public static readonly string TokenAudience = string.Empty;

        public static readonly double TokenLifetimeMinutes = 30;

        static SecurityConstants()
        {
            RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();
            cryptoProvider.GetNonZeroBytes(KeyForHmacSha256);   //Secure enough? Will change on every call. Has to be made a constant.

            TokenIssuer = "issuer"; //What should be a good value here? web api url?

            TokenAudience = "http://localhost:90";  //What should be a good value here?
        }
    }
}
