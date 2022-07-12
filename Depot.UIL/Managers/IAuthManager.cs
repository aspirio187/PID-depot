using Depot.UIL.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Depot.UIL.Managers
{
    public interface IAuthManager
    {
        string GenerateJwtToken(UserModel user);
        bool SendVerificationEmail(string toMail,Guid userID, string token);
        Task<bool> LogInAsync(string email, string password, bool rememberMe);
        Task LogOutAsync();
        bool IsSignedIn(ClaimsPrincipal claimsPrincipal);
    }
}
