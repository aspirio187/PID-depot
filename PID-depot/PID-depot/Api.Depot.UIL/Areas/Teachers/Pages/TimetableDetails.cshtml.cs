using Api.Depot.BLL.Dtos.LessonDetailDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

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

        public IActionResult OnGet(int id)
        {
            if (id == 0) return RedirectToPage("Index", new { Area = "Teachers" });

            LessonDetail = new LessonDetailForm()
            {
                LessonTimetableId = id,
            };

            return Page();
        }

        public IActionResult OnPost(List<IFormFile> postedFiles)
        {
            if (ModelState.IsValid)
            {
                //LessonDetailDto createdLessonDetails = _lessonDetailService.CreateLessonDetail(LessonDetail.MapToBLL());
                //if (createdLessonDetails is null)
                //{
                //    ModelState.AddModelError("Lesson Details Creation", "La création des détails du cours a echouée");
                //    _logger.LogError("Lesson details creation failed");
                //    return Page();
                //}

                var v = Path.GetFullPath(FilesData.FILE_DIRECTORY_PATH);

                foreach (var file in postedFiles)
                {

                }
            }

            return Page();
        }
    }
}
