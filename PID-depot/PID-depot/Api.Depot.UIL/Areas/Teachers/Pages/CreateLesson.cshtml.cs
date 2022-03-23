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
    // TODO : Fixed the problem described here
    // En cas d'erreur et que la page se recharge, la liste LessonDays est perdue et il faut la recharger ou recharger la page en retapant le lien

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = RolesData.AUTH_TEACHER_ROLE)]
    public class CreateLessonModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonService _lessonService;
        private readonly ILessonTimetableService _lessonTimetableService;

        [BindProperty]
        public LessonSiteForm Lesson { get; set; }

        [BindProperty]
        public List<LessonDayForm> LessonDays { get; set; }

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

            LessonDays = new List<LessonDayForm>();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                if (day != DayOfWeek.Sunday)
                {
                    LessonDays.Add(new LessonDayForm()
                    {
                        Day = (int)day,
                        DayName = DateTimeHelper.DayOfWeekToFrench(day),
                    });
                }
            }
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
                            // If the lesson couldn't be created, just returning a ModelState error
                            ModelState.AddModelError("Lesson creation", "La création du cours a échoué");
                            return Page();
                        }

                        if (!_lessonService.AddLessonUser(createdLesson.Id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))))
                        {
                            // If the user couldn't be added to the lesson we delete the lesson. If the lesson could not be deleted we log an error
                            ModelState.AddModelError("Add user", "Vous n'avez pas pu être ajouté comme professeur pour ce cours");
                            if (!_lessonService.DeleteLesson(createdLesson.Id))
                            {
                                _logger.LogError("Couldn't delete lesson with id : {0}", createdLesson.Id);
                            }
                            return Page();
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
                                // TODO : Si un horaire n'a pas pu être créer, supprimer tous les horaires relatifs, les liaisons avec les professeur et le cours
                                _logger.LogError("Timetable for day {0} at date {1} couldn't be created!",
                                    timetableToCreate.StartsAt.DayOfWeek.ToString(), timetableToCreate.StartsAt.ToString("dd-MM-yyyy"));
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
    }
}
