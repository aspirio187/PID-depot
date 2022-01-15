using Api.Depot.BLL.Dtos.RoleDtos;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            IEnumerable<RoleDto> rolesFromRepo = _roleService.GetRoles();
            return Ok(rolesFromRepo.Select(r => r.MapFromBLL()));
        }

        [HttpGet("{id}")]
        public IActionResult GetRole(Guid roleId)
        {
            if (roleId == Guid.Empty) return BadRequest(roleId);
            RoleDto roleFromRepo = _roleService.GetRole(roleId);
            if (roleFromRepo is null) return NotFound(roleId);
            return Ok(roleFromRepo.MapFromBLL());
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody] RoleForm role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            RoleDto createdRole = _roleService.CreateRole(role.MapToBLL());
            if (createdRole is null) return BadRequest(role);

            return Ok(createdRole.MapFromBLL());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Role ID cannot be null!");
            return Ok(_roleService.DeleteRole(id));
        }
    }
}
