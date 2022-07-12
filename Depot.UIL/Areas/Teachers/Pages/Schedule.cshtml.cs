using Depot.BLL.IServices;
using Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace Depot.UIL.Areas.Teachers.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = RolesData.AUTH_TEACHER_ROLE)]
    public class ScheduleModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly ILessonService _lessonService;
        private readonly ILessonTimetableService _lessonTimetable;

        public ScheduleModel(ILogger<ScheduleModel> logger, IUserService userService, ILessonService lessonService, ILessonTimetableService lessonTimetable)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
            _lessonTimetable = lessonTimetable ??
                throw new ArgumentNullException(nameof(lessonTimetable));
        }

        public void OnGet()
        {

        }
    }
}
