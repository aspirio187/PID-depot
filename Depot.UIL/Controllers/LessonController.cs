using Depot.BLL.Dtos.LessonDtos;
using Depot.BLL.Dtos.LessonTimetableDtos;
using Depot.BLL.Dtos.RoleDtos;
using Depot.BLL.Dtos.UserDtos;
using Depot.BLL.Dtos.UserLessonDtos;
using Depot.BLL.IServices;
using Depot.UIL.Models.Forms;
using Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly ILessonTimetableService _lessonTimetableService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public LessonController(ILessonService lessonService, IUserService userService, IRoleService roleService, ILessonTimetableService lessonTimetableService)
        {
            _lessonService = lessonService ??
                throw new ArgumentNullException(nameof(lessonService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));
            _lessonTimetableService = lessonTimetableService ??
                throw new ArgumentNullException(nameof(lessonTimetableService));
        }

        [HttpGet]
        public IActionResult GetLessons()
        {
            RoleDto teacherRole = _roleService.GetRole(RolesData.TEACHER_ROLE);
            if (teacherRole is null) return NotFound($"Teachers doesn't exist!");
            return Ok(_lessonService.GetLessons().Select(l => l.MapFromBLL(_userService.GetUserLesson(l.Id, teacherRole.Id))));
        }

        [HttpGet("{id}")]
        public IActionResult GetLesson(int id)
        {
            LessonDto lessonsFromRepo = _lessonService.GetLesson(id);
            if (lessonsFromRepo is null) return NotFound(id);

            RoleDto teacherRole = _roleService.GetRole(RolesData.TEACHER_ROLE);
            if (teacherRole is null) return NotFound("Teachers doesn't exist!");

            return Ok(lessonsFromRepo.MapFromBLL(_userService.GetUserLesson(lessonsFromRepo.Id, teacherRole.Id)));
        }

        [HttpGet("{id}/students")]
        public IActionResult GetLessonStudents(int id)
        {
            if (id == 0) return BadRequest(nameof(id));

            RoleDto role = _roleService.GetRole(RolesData.STUDENT_ROLE);
            if (role is null) return NotFound(RolesData.STUDENT_ROLE);
            IEnumerable<UserDto> users = _userService.GetUsersLesson(id, role.Id);

            return Ok(users.Select(u => u.MapFromBLL()));
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserLessons(Guid userId)
        {
            if (userId == Guid.Empty) return BadRequest($"{nameof(userId)} : {userId}");

            UserDto user = _userService.GetUser(userId);
            if (user is null) return NotFound(nameof(userId));
            IEnumerable<LessonDto> userLessons = _lessonService.GetUserLessons(userId);
            return Ok(userLessons.Select(u => u.MapFromBLL(user)));
        }

        [HttpGet("{lessonID}/timetables")]
        public IActionResult GetLessonTimetables(int lessonID)
        {
            if (lessonID == 0) return BadRequest(nameof(lessonID));

            var lessonTimetables = _lessonTimetableService.GetLessonTimetables(lessonID);
            return Ok(lessonTimetables.Select(lt => lt.MapFromBLL()));
        }

        [HttpPost]
        public IActionResult CreateLesson([FromBody] LessonForm lesson)
        {
            if (!ModelState.IsValid) return BadRequest(lesson);
            LessonDto createdLesson = _lessonService.CreateLesson(lesson.MapToBLL());
            if (createdLesson is null) return BadRequest(lesson);

            RoleDto teacherRole = _roleService.GetRole(RolesData.TEACHER_ROLE);

            if (teacherRole is null)
            {
                teacherRole = _roleService.CreateRole(new RoleCreationDto()
                {
                    Name = RolesData.TEACHER_ROLE
                });
            }

            if (teacherRole is null) return NotFound($"{nameof(teacherRole)} not found");

            if (!_lessonService.AddLessonUser(createdLesson.Id, lesson.UserId)) return BadRequest();

            return Ok(_lessonService.GetLesson(createdLesson.Id).MapFromBLL(_userService.GetUser(lesson.UserId)));
        }

        [HttpPost("{id}/{userId}")]
        public IActionResult Subscribe(int id, Guid userId)
        {
            if (id == 0) return BadRequest(nameof(id));
            if (userId == Guid.Empty) return BadRequest(nameof(userId));

            LessonDto lessonFromRepo = _lessonService.GetLesson(id);
            if (lessonFromRepo is null) return NotFound(id);

            var userFromRepo = _userService.GetUser(userId);
            if (userFromRepo is null) return NotFound(userId);
            if (userFromRepo.MapFromBLL(_roleService.GetUserRoles(userId)).Roles.Any(r => r.Name.Equals(RolesData.TEACHER_ROLE)))
                return BadRequest("The user is a teacher!");

            if (_lessonService.GetUserLessons(userId).Any(l => l.Id == id))
                return BadRequest($"The user is already subscribed to lesson : {lessonFromRepo.Name}");

            return Ok(_lessonService.AddLessonUser(id, userId));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RolesData.ADMIN_ROLE)]
        public IActionResult DeleteLesson(int id)
        {
            if (id == 0) return BadRequest(nameof(id));
            if (!_lessonService.DeleteLesson(id)) return NotFound(id);

            return Ok();
        }
    }
}
