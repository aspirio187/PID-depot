using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.LessonTimetableDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Helpers;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Api.Depot.UIL.Areas.Teachers.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = RolesData.AUTH_TEACHER_ROLE)]
    public class CreateLessonModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonService _lessonService;
        private readonly ILessonTimetableService _lessonTimetableService;

        [BindProperty]
        public LessonSiteForm Lesson { get; set; }

        [BindProperty]
        public List<LessonDayForm> LessonDays { get; set; } = LessonDaysData.LoadLessonDays().ToList();

        public CreateLessonModel(ILogger<CreateLessonModel> logger, ILessonService lessonService, ILessonTimetableService lessonTimetableService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
            _lessonTimetableService = lessonTimetableService ??
                throw new ArgumentNullException(nameof(lessonTimetableService));
        }

        public void OnGet()
        {
            Lesson = new LessonSiteForm()
            {
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
        }

        public IActionResult OnPost()
        {
            if (!LessonDays.Any(ld => ld.IsSelected))
            {
                ModelState.AddModelError("No timespan", "Vous devez choisir au moins un jour de cours avec un horaire définis!");
                return Page();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (User.IsInRole(RolesData.TEACHER_ROLE))
                    {
                        LessonDto createdLesson = _lessonService.CreateLesson(Lesson.MapToBLL());
                        if (createdLesson is null)
                        {
                            ModelState.AddModelError("Lesson creation", "La création du cours a échoué");
                            return Page();
                        }

                        if (!_lessonService.AddLessonUser(createdLesson.Id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))))
                        {
                            ModelState.AddModelError("Add user", "Vous n'avez pas pu être ajouté comme professeur pour ce cours");
                            return DeleteLesson(createdLesson.Id);
                        }

                        for (DateTime d = Lesson.StartsAt; d <= Lesson.EndsAt.AddDays(1); d = d.AddDays(1))
                        {
                            LessonDayForm lessonDay = LessonDays.SingleOrDefault(ld => ld.IsSelected && ld.Day == (int)d.DayOfWeek);
                            if (lessonDay is null) continue;

                            LessonTimetableCreationDto timetableToCreate = new LessonTimetableCreationDto()
                            {
                                LessonId = createdLesson.Id,
                                StartsAt = new DateTime(d.Year, d.Month, d.Day, lessonDay.StartsAt.Hours, lessonDay.StartsAt.Minutes, 0),
                                EndsAt = new DateTime(d.Year, d.Month, d.Day, lessonDay.EndsAt.Hours, lessonDay.EndsAt.Minutes, 0)
                            };

                            LessonTimetableDto createdTimetable = _lessonTimetableService.CreateLessonTimetable(timetableToCreate);

                            if (createdTimetable is null)
                            {
                                _logger.LogError("Timetable for day {0} at date {1} couldn't be created!",
                                    timetableToCreate.StartsAt.DayOfWeek.ToString(), timetableToCreate.StartsAt.ToString("dd-MM-yyyy"));

                                return DeleteLesson(createdLesson.Id);
                            }
                        }

                        return RedirectToPage("/Index", new { area = "Teachers" });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return Page();
                }
            }
            return Page();
        }

        /// <summary>
        /// Try to delete the lesson with id <paramref name="lessonId"/>. If it fails, an error is logged.
        /// </summary>
        /// <param name="lessonId">Lesson to delete id</param>
        /// <returns>Whatever happens, return this page</returns>
        private IActionResult DeleteLesson(int lessonId)
        {
            if (!_lessonService.DeleteLesson(lessonId))
            {
                _logger.LogError("Couldn't delete lesson with ID : {0}", lessonId);
            }
            return Page();
        }
    }
}
