using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RolesData.TEACHER_ROLE)]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));

            if (!InitilizeRoles()) throw new ApplicationException("Roles initialization didn't work!");
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            IEnumerable<RoleDto> rolesFromRepo = _roleService.GetRoles();
            return Ok(rolesFromRepo.Select(r => r.MapFromBLL()));
        }

        [HttpGet("{id}")]
        public IActionResult GetRole(Guid id)
        {
            if (id == Guid.Empty) return BadRequest(id);
            RoleDto roleFromRepo = _roleService.GetRole(id);
            if (roleFromRepo is null) return NotFound(id);
            return Ok(roleFromRepo.MapFromBLL());
        }

        private bool InitilizeRoles()
        {
            RoleDto userRole = _roleService.GetRole(RolesData.USER_ROLE);
            if (userRole is null) userRole = _roleService.CreateRole(new RoleCreationDto() { Name = RolesData.USER_ROLE });
            RoleDto studentRole = _roleService.GetRole(RolesData.STUDENT_ROLE);
            if (studentRole is null) studentRole = _roleService.CreateRole(new RoleCreationDto() { Name = RolesData.STUDENT_ROLE });
            RoleDto teacherRole = _roleService.GetRole(RolesData.TEACHER_ROLE);
            if (teacherRole is null) teacherRole = _roleService.CreateRole(new RoleCreationDto() { Name = RolesData.TEACHER_ROLE });
            RoleDto adminRole = _roleService.GetRole(RolesData.ADMIN_ROLE);
            if (adminRole is null) adminRole = _roleService.CreateRole(new RoleCreationDto() { Name = RolesData.ADMIN_ROLE });

            return userRole is not null && studentRole is not null
                && teacherRole is not null && adminRole is not null;
        }
    }
}
