using Api.Depot.UIL.Models;

namespace Api.Depot.UIL.Managers
{
    public interface IAuthManager
    {
        string GenerateJwtToken(UserModel user);
    }
}
