﻿using Api.Depot.BLL.Dtos.LessonFileDtos;
using Api.Depot.BLL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Depot.UIL.Controllers
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
    }
}