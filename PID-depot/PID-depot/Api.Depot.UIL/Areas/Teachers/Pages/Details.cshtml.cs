using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
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
        public List<LessonDayForm> LessonDays { get; set; } = new List<LessonDayForm>();

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

        public void OnGet(int id)
        {
            LessonDto lessonFromRepo = _lessonService.GetLesson(id);
            UserDto user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (lessonFromRepo is not null && user is not null)
            {
                Lesson = lessonFromRepo.MapFromBLL(user);

                var timetablesFromRepo = _lessonTimetableService.GetLessonTimetables(id).Take(7);
                var firstTimetable = timetablesFromRepo.FirstOrDefault();
                timetablesFromRepo = timetablesFromRepo.Where(lt => lt.EndsAt <= firstTimetable.EndsAt.AddDays(7));


            }
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
