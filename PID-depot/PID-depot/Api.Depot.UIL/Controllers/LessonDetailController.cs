using Api.Depot.BLL.Dtos.LessonDetailDtos;
using Api.Depot.BLL.Dtos.LessonFileDtos;
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
    public class LessonDetailController : ControllerBase
    {
        private readonly ILessonDetailService _lessonDetailService;
        private readonly ILessonFileService _lessonFileService;

        public LessonDetailController(ILessonDetailService lessonDetailService, ILessonFileService lessonFileService)
        {
            _lessonDetailService = lessonDetailService ??
                throw new ArgumentNullException(nameof(lessonDetailService));
            _lessonFileService = lessonFileService ??
                throw new ArgumentNullException(nameof(lessonFileService));
        }

        [HttpGet("{id}/files")]
        public IActionResult GetLessonDetailFiles(int id)
        {
            if (id == 0) return BadRequest(nameof(id));
            IEnumerable<LessonFileDto> filesFromRepo = _lessonFileService.GetLessonDetailFiles(id);
            return Ok(filesFromRepo);
        }

        [HttpPost]
        public IActionResult CreateLessonDetail(LessonDetailForm lessonDetail)
        {
            if (lessonDetail is null) return BadRequest(nameof(lessonDetail));
            if (!ModelState.IsValid) return BadRequest(nameof(lessonDetail));

            LessonDetailDto createdDetail = _lessonDetailService.CreateLessonDetail(lessonDetail.MapToBLL());
            if (createdDetail is null) return BadRequest(lessonDetail);
            return Ok(createdDetail.MapFromBLL());
        }
    }
}
