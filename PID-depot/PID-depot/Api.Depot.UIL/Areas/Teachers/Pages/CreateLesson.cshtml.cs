using Api.Depot.BLL.Dtos.LessonDtos;
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

        [BindProperty]
        public LessonForm Lesson { get; set; }

        public CreateLessonModel(ILogger<CreateLessonModel> logger, ILessonService lessonService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
        }

        public void OnGet()
        {
            Lesson = new LessonForm()
            {
                UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value)
            };
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.IsInRole(RolesData.TEACHER_ROLE) || User.IsInRole(RolesData.ADMIN_ROLE))
                    {
                        LessonDto createdLesson = _lessonService.CreateLesson(Lesson.MapToBLL());
                        if (createdLesson is null)
                        {
                            ModelState.AddModelError("Lesson creation", "La création du cours a échoué");
                            return Page();
                        }

                        RedirectToPage("/Index", new { area = "Teachers" });
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
