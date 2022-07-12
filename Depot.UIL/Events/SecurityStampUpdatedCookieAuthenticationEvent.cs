using Depot.BLL.IServices;
using Depot.UIL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Depot.UIL.Events
{
    public class SecurityStampUpdatedCookieAuthenticationEvent : CookieAuthenticationEvents
    {
        private readonly IUserService _userService;

        public SecurityStampUpdatedCookieAuthenticationEvent(IUserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            ClaimsPrincipal userPrincipal = context.Principal;
            string userStamp = userPrincipal.FindFirstValue("Stamp");
            string userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userStamp) || string.IsNullOrEmpty(userId))
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync();
            }

            UserModel user = _userService.GetUser(Guid.Parse(userId)).MapFromBLL();
            if (user is null || Guid.Parse(userStamp) != user.SecurityStamp)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync();
            }
        }
    }
}
