﻿using Api.Depot.UIL.Models;
using Mailjet.Client;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public bool ConnectionAsync(UserModel user)
        {
            string jwt = GenerateJwtToken(user);
            
            var claimIdentity = new ClaimsIdentity(jwt)
        }

        public bool SendVerificationEmail(string toMail, Guid userId, string token)
        {
            // TODO : Prévoir des urls pour la vérification
            // API .../Utilisateurs/{id}/Verify?token=token
            // ASP .../auth/activation?token=eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ

            try
            {
                MailAddress fromAdresse = new("pid.depot@gmail.com", "EICE Dépôt");
                MailAddress toAdresse = new(toMail, "To display name");
                const string fromPassword = "pazn gnsz llov qccp"; // PASSWORD 16 DIGITS. Se trouve sur bureau GMAIL_TOKEN_PASSWORD.png
                const string subject = "PID - Activitation du compte";

                StringBuilder body = new StringBuilder();
                body.Append("<h1>Bonjour \"Nom\"</h1>");
                body.Append("<p>Veuillez cliquer sur le lien ci-dessous pour activer votre compte</p>");

#if DEBUG
                body.Append($"<a href=\"https://localhost:44332/api/auth/activation?id={userId}&token={token}\">Cliquez-ici</a>");
#else
                body.Append($"<a href=\"https://www.domain.com/auth/activation?token={token}\">Cliquez-ici</a>");
#endif

                using (MailMessage mail = new())
                {
                    mail.From = fromAdresse;
                    mail.To.Add(toAdresse);
                    mail.Subject = subject;
                    mail.Body = body.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(fromAdresse.Address, fromPassword);
                        smtp.EnableSsl = true;
                        smtp.Timeout = 10000;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public void SendEmailWithAttachment(string[] toMails, string fichier)
        {
            try
            {
                using (MailMessage mail = new())
                {
                    mail.From = new MailAddress("mail@gmail.com", "From display name");

                    for (int i = 0; i < toMails.Length; i++)
                    {
                        mail.To.Add(new MailAddress(toMails[i]));
                    }

                    mail.Subject = "Test Mail - 1";
                    mail.Body = "mail with attachment";
                    mail.Attachments.Add(new Attachment(fichier));

                    using (SmtpClient smtp = new("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("your mail@gmail.com", "your password");
                        smtp.EnableSsl = true;
                        smtp.Timeout = 20000;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
