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
        public LessonUpdateForm LessonUpdate { get; set; } = new();

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

            var lesson = lessonFromRepo.MapFromBLL(user);

            IEnumerable<LessonTimetableDto> timetablesFromRepo = _lessonTimetableService.GetLessonTimetables(id);

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

            LessonUpdate.Description = lesson.Description;
            LessonUpdate.EndsAt = timetablesFromRepo.Last().EndsAt;
            LessonUpdate.Id = lesson.Id;

            TempData["LessonName"] = lesson.Name;
            TempData["LessonTeacher"] = lesson.TeacherName;
            TempData["LessonTeacherNumber"] = lesson.TeacherRegistrationNumber;
            TempData["LessonStartsAt"] = timetablesFromRepo.First().StartsAt;

            TempData.Keep();

            user = null;
            lesson = null;
            timetablesFromRepo = null;

            return Page();
        }

        public IActionResult OnPostModify()
        {
            if (ModelState.IsValid)
            {
                if (!LessonDays.Any(ld => ld.IsSelected == true))
                {
                    ModelState.AddModelError("Lesson days", "Vous devez choisir au moins un jour de cours avec une plage d'horaire");
                    return Page();
                }

                if (LessonUpdate.EndsAt <= (DateTime)TempData["LessonStartsAt"])
                {
                    ModelState.AddModelError("Lesson End", "La date de fin ne peut pas précéder ou égaler la date de début");
                    return Page();
                }

                // Etape 1 : Mettre à jours la description de la leçon - V
                // Etape 2 : Supprimer tous les horaires à partir de maintenant - V
                // Etape 3 : Recréer tous les horaires à partir de maintenant selon les jours définit jusqu'à la date de fin + 1 jour - V

                LessonDto lessonFromRepo = _lessonService.GetLesson(LessonUpdate.Id);
                if (lessonFromRepo is null)
                {
                    ModelState.AddModelError("Lesson ID", "Une erreur est survenue lors de la modification de la description");
                    _logger.LogError("There is no lesson with ID : {0}", LessonUpdate.Id);
                    return Page();
                }

                lessonFromRepo.Description = LessonUpdate.Description;

                LessonDto updatedLesson = _lessonService.UpdateLesson(lessonFromRepo);

                if (updatedLesson is null)
                {
                    ModelState.AddModelError("Lesson Update", "Une erreur est survenue lors de la modification de la leçon");
                    _logger.LogError("The lesson with ID : {0} couldn't be updated!", LessonUpdate.Id);
                    return Page();
                }

                IEnumerable<LessonTimetableDto> lessonTimetableFromRepo = _lessonTimetableService.GetLessonTimetables(LessonUpdate.Id)
                                                                                                    .Where(lt => lt.StartsAt >= DateTime.Now);

                foreach (LessonTimetableDto timetable in lessonTimetableFromRepo)
                {
                    if (!_lessonTimetableService.DeleteLessonTimetable(timetable.Id))
                    {
                        _logger.LogError("Timetable with ID : {0} couldn't be deleted!", timetable.Id);
                    }
                }

                for (DateTime d = DateTime.Now.AddDays(1); d <= LessonUpdate.EndsAt.AddDays(1); d = d.AddDays(1))
                {
                    LessonDayForm lessonDay = LessonDays.SingleOrDefault(ld => ld.IsSelected && ld.Day == (int)d.DayOfWeek);
                    if (lessonDay is null) continue;

                    LessonTimetableCreationDto timetableToCreate = new LessonTimetableCreationDto()
                    {
                        LessonId = LessonUpdate.Id,
                        StartsAt = new DateTime(d.Year, d.Month, d.Day, lessonDay.StartsAt.Hours, lessonDay.StartsAt.Minutes, 0),
                        EndsAt = new DateTime(d.Year, d.Month, d.Day, lessonDay.EndsAt.Hours, lessonDay.EndsAt.Minutes, 0)
                    };

                    LessonTimetableDto createdTimetable = _lessonTimetableService.CreateLessonTimetable(timetableToCreate);

                    if (createdTimetable is null)
                    {
                        _logger.LogError("Timetable for day {0} at date {1} couldn't be created!",
                            timetableToCreate.StartsAt.DayOfWeek.ToString(), timetableToCreate.StartsAt.ToString("dd-MM-yyyy"));
                    }
                }

                TempData.Clear();
                return RedirectToPage("/Index", new { Area = "Teachers" });
            }
            return Page();
        }

        public IActionResult OnPostDelete()
        {
            if (_lessonService.DeleteLesson(LessonUpdate.Id))
            {
                return RedirectToPage("/Index", new { Area = "Teachers" });
            }

            ModelState.AddModelError("Delete", "La suppression a échouée");
            return Page();
        }
    }
}
