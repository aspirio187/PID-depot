using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL.Areas.Teachers.Pages
{
    public class DetailsUpdateModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly ILessonDetailService _lessonDetailService;
        private readonly ILessonFileService _lessonFileService;


        [BindProperty]
        public LessonDetailModel LessonDetails { get; set; }

        private List<LessonFileModel> _lessonFiles;

        public IEnumerable<LessonFileModel> LessonFiles
        {
            get
            {
                if (_lessonFileService is not null && LessonDetails is not null)
                {
                    _lessonFiles = _lessonFileService.GetLessonDetailFiles(LessonDetails.Id).Select(lf => lf.MapFromBLL()).ToList();
                }
                return _lessonFiles;
            }
        }


        public DetailsUpdateModel(ILogger<DetailsUpdateModel> logger, ILessonDetailService lessonDetailService, ILessonFileService lessonFileService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _lessonDetailService = lessonDetailService ??
                throw new ArgumentNullException(nameof(lessonDetailService));
            _lessonFileService = lessonFileService ??
                throw new ArgumentNullException(nameof(lessonFileService));
        }

        public IActionResult OnGet(int id)
        {
            if (id == 0) return RedirectToPage("/Index", new { Area = "Teachers" });

            LessonDetails = _lessonDetailService.GetLessonDetail(id).MapFromBLL();

            if (LessonDetails is null) return RedirectToPage("/Index", new { Area = "Teachers" });

            return Page();
        }

        public IActionResult OnPostUpdate(List<IFormFile> postedFiles)
        {

            return Page();
        }

        public IActionResult OnPostDelete()
        {
            return Page();
        }
    }
}
