using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL.Pages.Student
{
    public class LessonDetailsModel : PageModel
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

        public LessonDetailsModel(ILogger<LessonDetailsModel> logger, ILessonDetailService lessonDetailService, ILessonFileService lessonFileService)
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
            if (id == 0) return RedirectToPage("Schedule");

            LessonDetails = _lessonDetailService.GetLessonDetail(id).MapFromBLL();

            if (LessonDetails is null) ViewData["EmptyDetails"] = "Il n'y a pas encore de détails pour ce cours";

            return Page();
        }
    }
}
