using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.LessonTimetableDtos;
using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Helpers;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Api.Depot.UIL.Areas.Teachers.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonService _lessonService;
        private readonly ILessonTimetableService _lessonTimetableService;
        private readonly IUserService _userService;

        [BindProperty]
        public LessonModel Lesson { get; set; }

        [BindProperty]
        public List<LessonDayForm> LessonDays { get; set; } = LessonDaysData.LoadLessonDays().ToList();

        public DetailsModel(ILogger<DetailsModel> logger, ILessonService lessonService, ILessonTimetableService lessonTimetableService, IUserService userService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
            _lessonTimetableService = lessonTimetableService ??
                throw new ArgumentNullException(nameof(lessonTimetableService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        public IActionResult OnGet(int id)
        {
            LessonDto lessonFromRepo = _lessonService.GetLesson(id);
            UserDto user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (lessonFromRepo is null || user is null) return RedirectToPage("/Index", new { area = "Teachers" });

            Lesson = lessonFromRepo.MapFromBLL(user);

            IEnumerable<LessonTimetableDto> timetablesFromRepo = _lessonTimetableService.GetLessonTimetables(id).Take(7);

            foreach (LessonTimetableDto timetable in timetablesFromRepo)
            {
                LessonDayForm ld = LessonDays.FirstOrDefault(d => d.Day == (int)timetable.StartsAt.DayOfWeek);
                if (ld is null)
                {
                    throw new NullReferenceException($"There must be at least one day in {nameof(LessonDays)} " +
                        $"that matches the day {timetable.StartsAt.DayOfWeek}");
                }

                if (ld.IsSelected == true && ld.StartsAt == timetable.StartsAt.TimeOfDay && ld.EndsAt == timetable.EndsAt.TimeOfDay)
                {
                    break;
                }

                ld.IsSelected = true;
                ld.StartsAt = timetable.StartsAt.TimeOfDay;
                ld.EndsAt = timetable.EndsAt.TimeOfDay;
                ld.DayName = DateTimeHelper.DayOfWeekToFrench(timetable.StartsAt.DayOfWeek);
            }

            timetablesFromRepo = null;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("/Index", new { area = "Teachers" });
            }

            return Page();
        }
    }
}
