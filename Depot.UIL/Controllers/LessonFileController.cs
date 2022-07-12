using Depot.BLL.Dtos.LessonFileDtos;
using Depot.BLL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonFileController : ControllerBase
    {
        private readonly ILessonDetailService _lessonDetailService;
        private readonly ILessonFileService _lessonFileService;
        private readonly string PATH = System.IO.Path.GetFullPath("Files/");

        public LessonFileController(ILessonFileService lessonFileService, ILessonDetailService lessonDetailService)
        {
            _lessonFileService = lessonFileService ??
                throw new ArgumentNullException(nameof(lessonFileService));
            _lessonDetailService = lessonDetailService ??
                throw new ArgumentNullException(nameof(lessonDetailService));
        }

        [HttpPost("{id}")]
        public IActionResult CreateFile(int id, IFormFile file)
        {
            if (file.Length <= 0) return BadRequest(file);
            if (_lessonDetailService.GetDetail(id) is null) return NotFound(id);

            string fileName = $"{DateTime.Now}.{file.FileName}";
            string filePath = $"{PATH}{fileName}";

            using (System.IO.FileStream stream = System.IO.File.Create(filePath))
            {
                if (stream is null) return BadRequest();
                file.CopyTo(stream);
            }

            LessonFileDto createdFile = _lessonFileService.CreateLessonFile(new LessonFileCreationDto()
            {
                FilePath = filePath,
                LessonDetailId = id
            });

            if (createdFile is null) return BadRequest(file);

            return Ok(createdFile.MapFromBLL());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFile(int id)
        {
            if (id == 0) return BadRequest(nameof(id));

            LessonFileDto fileFromRepo = _lessonFileService.GetFile(id);
            if (fileFromRepo is null) return NotFound(nameof(id));

            System.IO.File.Delete(fileFromRepo.FilePath);
            if (System.IO.File.Exists(fileFromRepo.FilePath))
            {
                return BadRequest(fileFromRepo.FilePath);
            }

            if (!_lessonFileService.DeleteLessonFile(id))
            {
                return BadRequest(nameof(id));
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult DownloadFile(int id)
        {
            if (id == 0) return BadRequest();

            LessonFileDto fileFromRepo = _lessonFileService.GetFile(id);

            if (fileFromRepo is null) return NotFound(nameof(id));

            string contentType = MediaTypeNames.Application.Octet;
            if (contentType is null) contentType = "file";

            bool fileExist = System.IO.File.Exists(fileFromRepo.FilePath);
            if (fileExist == false) return NotFound(fileFromRepo.FilePath.Split('\\').Last());

            FileStream fs = System.IO.File.OpenRead(fileFromRepo.FilePath);

            return File(fs, contentType, fileFromRepo.FilePath.Split('\\').Last());
        }
    }
}
