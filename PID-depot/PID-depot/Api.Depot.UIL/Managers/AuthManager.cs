using Api.Depot.UIL.Models;
using Mailjet.Client;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Api.Depot.UIL.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly JwtModel _jwtModel;

        public AuthManager(IOptions<JwtModel> jwtModel)
        {
            _jwtModel = jwtModel.Value ??
                throw new ArgumentNullException(nameof(jwtModel.Value));
        }

        public string GenerateJwtToken(UserModel user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (user.Roles is null) throw new ArgumentNullException(nameof(user.Roles));
            if (user.Roles.Count() == 0) throw new ArgumentException($"{nameof(user.Roles)} can not be empty");

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            IEnumerable<Claim> roleClaims = user.Roles.Select(ur => new Claim(ClaimTypes.Role, ur.Name));

            claims.AddRange(roleClaims);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtModel.Secret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime tokenExpiration = DateTime.Now.AddDays(_jwtModel.ExpirationInDays);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtModel.Issuer,
                audience: _jwtModel.Audience,
                claims,
                expires: tokenExpiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void SendVerificationEmail(string email)
        {
            MailjetClient client = new MailjetClient()
        }
    }

    internal class MailJetCredential
    {
        public string SecretKey { get; set; }
        public string ApiKey { get; set; }
    }
}
