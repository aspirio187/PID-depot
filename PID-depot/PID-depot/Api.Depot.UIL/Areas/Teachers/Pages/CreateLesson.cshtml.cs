using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.LessonTimetableDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
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
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.IsInRole(RolesData.TEACHER_ROLE))
                    {
                        LessonDto createdLesson = _lessonService.CreateLesson(Lesson.MapToBLL());
                        if (createdLesson is null)
                        {
                            ModelState.AddModelError("Lesson creation", "La cr�ation du cours a �chou�");
                            return Page();
                        }

                        if (!_lessonService.AddLessonUser(createdLesson.Id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))))
                        {
                            ModelState.AddModelError("Add user", "Vous n'avez pas pu �tre ajout� comme professeur pour ce cours");
                            if(_lessonService.DeleteLesson(createdLesson.Id))
                            {
                                _logger.LogError("Couldn't delete lesson with id : {0}", createdLesson.Id);
                            }
                            return Page();
                        }

                        LessonTimetableDto createdLessonTimetable = _lessonTimetableService.CreateLessonTimetable(Lesson.MapToBLL(createdLesson.Id));
                        if (createdLessonTimetable is null)
                        {
                            ModelState.AddModelError("Timetable creation", "L'horaire cr�e n'a pas pu �tre ajout� au cours");

                            return Page();
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
