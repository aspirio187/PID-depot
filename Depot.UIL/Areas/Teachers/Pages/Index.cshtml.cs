using Depot.BLL.Dtos.UserDtos;
using Depot.BLL.IServices;
using Depot.UIL.Models;
using Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Depot.UIL.Areas.Teachers.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = RolesData.AUTH_TEACHER_ROLE)]
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonService _lessonService;
        private readonly IUserService _userService;

        public IEnumerable<LessonModel> Lessons { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ILessonService lessonService, IUserService userService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }


        public void OnGet()
        {
            if (User is not null)
            {
                string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(id))
                {
                    Guid userId = Guid.Parse(id);
                    UserDto teacher = _userService.GetUser(userId);
                    Lessons = _lessonService.GetUserLessons(userId).Select(l => l.MapFromBLL(teacher));
                }
            }
        }
    }
}
