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
    public class SearchModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonService _lessonService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        private readonly int ElementsPerPage = 5;

        public string Query { get; set; }
        public int ActualPage { get; set; }
        public bool HasPrevious { get; set; } = false;
        public bool HasNext { get; set; } = false;

        public IEnumerable<LessonModel> SearchResult { get; set; }

        public SearchModel(ILogger<SearchModel> logger, ILessonService lessonService, IUserService userService, IRoleService roleService)
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

        public void OnGet(string q, int p = 1)
        {
            Query = q;
            ActualPage = p;

            RoleDto teacherRole = _roleService.GetRole(RolesData.TEACHER_ROLE);

            IEnumerable<LessonDto> result = _lessonService.GetLessons()
                            .Where(l => l.Name.Contains(q, StringComparison.InvariantCultureIgnoreCase) ||
                                        l.Description.Contains(q, StringComparison.InvariantCultureIgnoreCase));

            if (ActualPage > 1) HasPrevious = true;
            if (p * ElementsPerPage < result.Count()) HasNext = true;

            SearchResult = result.Skip(p - 1 * ElementsPerPage)
                                    .Take(ElementsPerPage)
                                    .Select(l => l.MapFromBLL(_userService.GetUserLesson(l.Id, teacherRole.Id)));
        }
    }
}
