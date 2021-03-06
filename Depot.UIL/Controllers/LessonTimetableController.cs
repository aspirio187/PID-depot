using Depot.BLL.Dtos.LessonTimetableDtos;
using Depot.BLL.IServices;
using Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTimetableController : ControllerBase
    {
        private readonly ILessonTimetableService _lessonTimetableService;
        private readonly ILessonDetailService _lessonDetailService;

        public LessonTimetableController(ILessonTimetableService lessonTimetableService, ILessonDetailService lessonDetailService)
        {
            _lessonTimetableService = lessonTimetableService ??
                throw new ArgumentNullException(nameof(lessonTimetableService));
            _lessonDetailService = lessonDetailService ??
                throw new ArgumentNullException(nameof(lessonDetailService));
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

        [HttpGet("{id}/details")]
        public IActionResult GetTimetableDetails(int id)
        {
            if (id == 0) return BadRequest(nameof(id));
            var detailsFromRepo = _lessonDetailService.GetLessonDetails(id);
            return Ok(detailsFromRepo.Select(d => d.MapFromBLL()).ToList());
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
