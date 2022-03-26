using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL.Pages
{
    public class LessonsModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonService _lessonService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        private readonly int ElementsPerPage = 10;

        public IEnumerable<LessonModel> Lessons { get; set; }
        public int ActualPage { get; set; }
        public bool HasPrevious { get; set; } = false;
        public bool HasNext { get; set; } = false;

        public LessonsModel(ILogger<LessonsModel> logger, ILessonService lessonService, IUserService userService, IRoleService roleService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));
        }

        public IActionResult OnGet(int p = 1)
        {
            RoleDto teacherRole = _roleService.GetRole(RolesData.TEACHER_ROLE);
            if (teacherRole is null) return RedirectToPage("./Error");

            ActualPage = p;
            if (p != 1) HasPrevious = true;

            IEnumerable<LessonDto> lessons = _lessonService.GetLessons();

            if (p * ElementsPerPage < lessons.Count()) HasNext = true;

            Lessons = lessons.Skip(p - 1 * ElementsPerPage).Take(10).Select(l => l.MapFromBLL(_userService.GetUserLesson(l.Id, teacherRole.Id)));

            return Page();
        }
    }
}
