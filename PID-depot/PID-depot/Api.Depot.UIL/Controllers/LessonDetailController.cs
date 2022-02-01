using Api.Depot.BLL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Api.Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonDetailController : ControllerBase
    {
        private readonly ILessonDetailService _lessonDetailService;

        public LessonDetailController(ILessonDetailService lessonDetailService)
        {
            _lessonDetailService = lessonDetailService ??
                throw new ArgumentNullException(nameof(lessonDetailService));
        }

        [HttpGet("{id}")]
        public IActionResult GetLessonDetails(int id)
        {
            if (id == 0) return BadRequest(nameof(id));
            var detailsFromRepo = 
        }
    }
}
