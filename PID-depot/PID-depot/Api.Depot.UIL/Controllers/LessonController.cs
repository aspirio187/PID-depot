using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
        }

        [HttpGet]
        public IActionResult GetLessons()
        {
            return Ok(_lessonService.GetLessons().Select(l => l.MapFromBLL()));
        }

        [HttpGet("{id}")]
        public IActionResult GetLesson(int id)
        {
            LessonDto lessonsFromRepo = _lessonService.GetLesson(id);
            if (lessonsFromRepo is null) return BadRequest(id);

            return Ok(lessonsFromRepo.MapFromBLL());
        }


    }
}
