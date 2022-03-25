using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Depot.UIL.Pages.Student
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = RolesData.AUTH_STUDENT_ROLE)]
    public class ScheduleModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
