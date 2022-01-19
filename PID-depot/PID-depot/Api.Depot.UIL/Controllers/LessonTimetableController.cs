using Api.Depot.BLL.Dtos.LessonTimetableDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Api.Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTimetableController : ControllerBase
    {
        private readonly ILessonTimetableService _lessonTimetableService;

        public LessonTimetableController(ILessonTimetableService lessonTimetableService)
        {
            _lessonTimetableService = lessonTimetableService ??
                throw new ArgumentNullException(nameof(lessonTimetableService));
        }

        // TODO : Les routes de récupérations
        [HttpGet]
        public IActionResult GetTimetables()
        {
            return Ok(_lessonTimetableService.GetTimetables().Select(lt => lt.MapFromBLL()));
        }

        [HttpGet("{id}")]
        public IActionResult GetTimetable(int id)
        {
            if (id == 0) return BadRequest(nameof(id));
            LessonTimetableDto timeTableFromRepo = _lessonTimetableService.GetTimetable(id);
            if (timeTableFromRepo is null) return NotFound(id);

            return Ok(timeTableFromRepo.MapFromBLL());
        }

        [HttpPost()]
        public IActionResult CreateLessonTimetable([FromBody] LessonTimetableForm lessonTimetable)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            LessonTimetableDto createdLessonTimetable = _lessonTimetableService.CreateLessonTimetable(lessonTimetable.MapToBLL());
            if (createdLessonTimetable is null) return BadRequest(lessonTimetable);

            return Ok(createdLessonTimetable.MapFromBLL());
        }
    }
}
