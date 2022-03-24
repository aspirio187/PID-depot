using Api.Depot.BLL.Dtos.LessonDetailDtos;
using Api.Depot.BLL.Dtos.LessonFileDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
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
            if (!ModelState.IsValid) return Page();

            LessonDetailDto updatedLessonDetails = _lessonDetailService.UpdateLessonDetail(LessonDetails.MapToBLL());
            if (updatedLessonDetails is null)
            {
                ModelState.AddModelError("Lesson details update", "La mise à jours des détails du cours a échoué");
                return Page();
            }

            // Etape 1 : Déplacer les fichiers existant
            // Etape 2 : Sauvegarder les nouveaux fichiers en écrasant les fichiers existant

            string directoryPath = $"{Path.GetFullPath(FilesData.FILE_DIRECTORY_PATH)}\\{updatedLessonDetails.Title}\\";
            string oldDirectoryPath = Path.GetDirectoryName(LessonFiles.First().FilePath);

            if (!directoryPath.Equals(oldDirectoryPath))
            {
                foreach (LessonFileModel lessonFile in LessonFiles)
                {
                    lessonFile.FilePath = Path.Combine(directoryPath, Path.GetFileName(lessonFile.FilePath));
                    LessonFileDto updatedLessonFile = _lessonFileService.UpdateLessonFile(lessonFile.MapToBLL());
                    if (updatedLessonFile is null)
                    {
                        _logger.LogError("Couldn't update lesson file with ID : {0}", lessonFile.Id);
                        return Page();
                    }
                }

                FilesData.MoveFilesFromFolder(oldDirectoryPath, directoryPath);
            }

            foreach (IFormFile file in postedFiles)
            {
                string fileName = Path.GetFileName(file.FileName);

                LessonFileDto fileToCreate = _lessonFileService.CreateLessonFile(new LessonFileCreationDto()
                {
                    FilePath = Path.Combine(directoryPath, fileName),
                    LessonDetailId = updatedLessonDetails.Id
                });

                if (fileToCreate is null)
                {
                    ModelState.AddModelError("File Save", $"La sauvegarde du fichier {file.Name} a échouée");
                    _logger.LogError("File save failed for file {0}", file.Name);
                    return Page();
                }

                try
                {
                    FilesData.SaveFilesToFolder(file, directoryPath);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    _lessonFileService.DeleteLessonFile(fileToCreate.Id);
                    return Page();
                }
            }

            return Page();
        }

        public IActionResult OnPostDelete()
        {
            if (_lessonDetailService.DeleteLessonDetail(LessonDetails.Id))
            {


                return RedirectToPage("/Index", new { Area = "Teachers" });
            }
            return Page();
        }
    }
}
