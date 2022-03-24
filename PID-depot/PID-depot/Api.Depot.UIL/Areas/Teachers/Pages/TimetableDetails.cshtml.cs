using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Api.Depot.UIL.Areas.Teachers.Pages
{
    public class TimetableDetailsModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonFileService _lessonFileService;
        private readonly ILessonDetailService _lessonDetailService;

        public TimetableDetailsModel(ILogger<TimetableDetailsModel> logger, ILessonFileService lessonFileService, ILessonDetailService lessonDetailService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _lessonFileService = lessonFileService ??
                throw new ArgumentNullException(nameof(lessonFileService));
            _lessonDetailService = lessonDetailService ??
                throw new ArgumentNullException(nameof(lessonDetailService));
        }

        [BindProperty]
        public LessonDetailForm LessonDetail { get; set; }

        public List<LessonFileForm> LessonFile { get; set; }

        public void OnGet(int id)
        {
            if (id != 0)
            {
                LessonDetail = new LessonDetailForm()
                {
                    LessonTimetableId = id,
                };
            }
        }

        public IActionResult OnPost(List<IFormFile> postedFiles)
        {
            if (ModelState.IsValid)
            {

            }

            return Page();
        }
    }
}
