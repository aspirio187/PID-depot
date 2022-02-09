using Api.Depot.UIL.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Depot.UIL.Managers
{
    public interface IAuthManager
    {
        string GenerateJwtToken(UserModel user);
        bool SendVerificationEmail(string toMail,Guid userID, string token);
        Task<bool> LogInAsync(UserModel user);
        Task LogOutAsync();
        bool IsSignedIn(ClaimsPrincipal claimsPrincipal);
    }
}
