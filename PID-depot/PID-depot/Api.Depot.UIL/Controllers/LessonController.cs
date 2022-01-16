using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models.Forms;
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
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public LessonController(ILessonService lessonService, IUserService userService, IRoleService roleService)
        {
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));
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
            if (lessonsFromRepo is null) return NotFound(id);

            return Ok(lessonsFromRepo.MapFromBLL());
        }

        [HttpPost]
        public IActionResult CreateLesson([FromBody] LessonForm lesson)
        {
            if (!ModelState.IsValid) return BadRequest(lesson);
            LessonDto createdLesson = _lessonService.CreateLesson(lesson.MapToBLL());
            if (createdLesson is null) return BadRequest(lesson);

            // TODO : Add teacher
        }
    }
}
