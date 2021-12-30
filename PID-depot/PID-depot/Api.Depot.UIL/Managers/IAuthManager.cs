using Api.Depot.UIL.Models;
using System;

namespace Api.Depot.UIL.Managers
{
    public interface IAuthManager
    {
        string GenerateJwtToken(UserModel user);
        bool SendVerificationEmail(string toMail,Guid userID, string token);
    }
}
