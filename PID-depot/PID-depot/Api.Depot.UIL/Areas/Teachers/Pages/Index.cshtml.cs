using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL.Areas.Teachers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonService _lessonService;

        public IEnumerable<LessonModel> Lessons { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ILessonService lessonService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
        }


        public void OnGet()
        {
            Lessons = _lessonService.GetLessons().Where(l => l.)
        }
    }
}
