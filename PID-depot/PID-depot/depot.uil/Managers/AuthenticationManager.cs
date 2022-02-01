
using Depot.UIL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Depot.UIL.Managers
{
    public class AuthenticationManager
    {
        private readonly HttpContext _httpContext;

        public UserModel User { get; private set; }

        public AuthenticationManager(HttpContext httpContext)
        {
            _httpContext = httpContext ??
                throw new ArgumentNullException(nameof(httpContext));
        }

        public async Task<bool> SignIn(SignInModel signInModel)
        {
            if(signInModel is null) throw new ArgumentNullException(nameof(signInModel));
            // TODO : Call api

            UserModel user = new UserModel();
        }

        public bool UserIsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal is null) throw new ArgumentNullException(nameof(claimsPrincipal));


            return false;
        }
    }
}
