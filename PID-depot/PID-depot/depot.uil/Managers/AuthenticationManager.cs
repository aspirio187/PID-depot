
using Depot.UIL.Models;

namespace Depot.UIL.Managers
{
    public class AuthenticationManager
    {
        public UserModel User { get; private set; }

        public bool IsSignedIn
        {
            get
            {
                // TODO : Vérifier s'il existe un token jwt
                return false;
            }
        }

        public AuthenticationManager()
        {

        }
    }
}
