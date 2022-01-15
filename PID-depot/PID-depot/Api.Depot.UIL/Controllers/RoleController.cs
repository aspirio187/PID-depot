using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
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
        public IActionResult GetRole(Guid id)
        {
            if (id == Guid.Empty) return BadRequest(id);
            RoleDto roleFromRepo = _roleService.GetRole(id);
            if (roleFromRepo is null) return NotFound(id);
            return Ok(roleFromRepo.MapFromBLL());
        }

        //[HttpPost]
        //public IActionResult CreateRole([FromBody] RoleForm role)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    RoleDto createdRole = _roleService.CreateRole(role.MapToBLL());
        //    if (createdRole is null) return BadRequest(role);

        //    return Ok(createdRole.MapFromBLL());
        //}

        //[HttpPut()]
        //public IActionResult UpdateRole([FromBody] RoleModel role)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    RoleDto updatedRole = _roleService.UpdateRole(role.MapToBLL());
        //    if (updatedRole is null) return BadRequest(role);

        //    return Ok(updatedRole.MapFromBLL());
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Role ID cannot be null!");
            return Ok(_roleService.DeleteRole(id));
        }
    }
}
