using Api.Depot.BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace Api.Depot.UIL.Areas.Teachers.Pages
{
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
