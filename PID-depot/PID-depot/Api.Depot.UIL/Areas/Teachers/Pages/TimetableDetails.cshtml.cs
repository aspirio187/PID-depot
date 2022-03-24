using Api.Depot.BLL.Dtos.LessonDetailDtos;
using Api.Depot.BLL.Dtos.LessonFileDtos;
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
using System.Linq;

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

        public IActionResult OnGet(int id)
        {
            if (id == 0) return RedirectToPage("Index", new { Area = "Teachers" });

            if (_lessonDetailService.GetDetails().Any(ld => ld.LessonTimetableId == id))
            {
                return RedirectToPage("DetailsUpdate", new { Area = "Teachers", id = id });
            }

            LessonDetail = new LessonDetailForm()
            {
                LessonTimetableId = id,
            };

            return Page();
        }

        public IActionResult OnPost(List<IFormFile> postedFiles)
        {
            if (!ModelState.IsValid) return Page();

            LessonDetailDto createdLessonDetails = _lessonDetailService.CreateLessonDetail(LessonDetail.MapToBLL());
            if (createdLessonDetails is null)
            {
                ModelState.AddModelError("Lesson Details Creation", "La création des détails du cours a echouée");
                _logger.LogError("Lesson details creation failed");
                return Page();
            }

            string directoryFullPath = $"{Path.GetFullPath(FilesData.FILE_DIRECTORY_PATH)}\\{createdLessonDetails.Title}\\";

            foreach (IFormFile file in postedFiles)
            {
                string fileName = Path.GetFileName(file.FileName);

                LessonFileDto fileToCreate = _lessonFileService.CreateLessonFile(new LessonFileCreationDto()
                {
                    FilePath = Path.Combine(directoryFullPath, fileName),
                    LessonDetailId = createdLessonDetails.Id
                });

                if (fileToCreate is null)
                {
                    ModelState.AddModelError("File Save", $"La sauvegarde du fichier {file.Name} a échouée");
                    _logger.LogError("File save failed for file {0}", file.Name);
                    return Page();
                }

                try
                {
                    FilesData.SaveFilesToFolder(file, directoryFullPath);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    _lessonFileService.DeleteLessonFile(fileToCreate.Id);
                    return Page();
                }
            }

            return RedirectToPage("Schedule", new { Area = "Teachers" });
        }
    }
}
