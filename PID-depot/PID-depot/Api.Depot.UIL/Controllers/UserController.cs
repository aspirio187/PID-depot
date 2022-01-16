using Api.Depot.BLL.Dtos;
using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));

        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            UserDto userFromService = _userService.GetUser(id);
            if (userFromService is null) return NotFound(id);
            return Ok(userFromService.MapFromBLL(_roleService.GetUserRoles(userFromService.Id)));
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            IEnumerable<UserDto> usersFromService = _userService.GetUsers();
            return Ok(usersFromService.Select(u => u.MapFromBLL(_roleService.GetUserRoles(u.Id))));
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserForm user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            UserDto updatedUser = _userService.UpdateUser(user.MapToBLL());
            if (updatedUser is null) return BadRequest(user);
            return Ok(updatedUser.MapFromBLL(_roleService.GetUserRoles(updatedUser.Id)));
        }

        // N'autoriser l'accès qu'aux administrateur
        [HttpPost("{id}/{roleId}")]
        public IActionResult AddUserRole(Guid id, [FromBody] Guid roleId)
        {
            return Ok(_userService.AddUserRole(id, roleId));
        }

        // N'autoriser l'accès qu'aux administrateurs
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteUser(Guid id)
        {
            UserDto userFromRepo = _userService.GetUser(id);
            if (userFromRepo is null) return NotFound(id);
            return Ok(_userService.DeleteUser(id));
        }

        [HttpGet("Email/{email}")]
        public IActionResult IsEmailAvailable(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("Email cannot be null or empty!");

            return Ok(!_userService.EmailExist(email));
        }

        [HttpPost("Password")]
        public IActionResult UpdatePassword([FromBody] PasswordForm password)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_userService.UpdatePassword(password.Id, password.OldPassword, password.NewPassword))
                return BadRequest();

            return Ok(_userService.GetUser(password.Id));
        }
    }
}
