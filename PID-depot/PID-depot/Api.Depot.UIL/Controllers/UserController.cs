using Api.Depot.BLL.Dtos;
using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.Dtos.UserDtos;
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
using System.Security.Claims;

namespace Api.Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string Both = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme;

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
        [Authorize(CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RolesData.ADMIN_ROLE)]
        public IActionResult GetUsers()
        {
            IEnumerable<UserDto> usersFromService = _userService.GetUsers();
            return Ok(usersFromService.Select(u => u.MapFromBLL(_roleService.GetUserRoles(u.Id))));
        }

        [HttpPut]
        [Authorize(CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateUser([FromBody] UserForm user)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!userId.Equals(user.Id.ToString()) && !User.IsInRole(RolesData.ADMIN_ROLE)) return Unauthorized();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            UserDto updatedUser = _userService.UpdateUser(user.MapToBLL());
            if (updatedUser is null) return BadRequest(user);
            return Ok(updatedUser.MapFromBLL(_roleService.GetUserRoles(updatedUser.Id)));
        }

        [HttpPost("{id}/{roleId}")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RolesData.ADMIN_ROLE)]
        public IActionResult AddUserRole(Guid id, Guid roleId)
        {
            RoleDto role = _roleService.GetRole(roleId);
            IEnumerable<RoleDto> userRoles = _roleService.GetUserRoles(id);

            if ((userRoles.Any(ur => ur.Name.Equals(RolesData.STUDENT_ROLE)) && role.Name.Equals(RolesData.TEACHER_ROLE)) ||
                (userRoles.Any(ur => ur.Name.Equals(RolesData.TEACHER_ROLE)) && role.Name.Equals(RolesData.STUDENT_ROLE)))
            {
                return BadRequest(role.Name);
            }

            return Ok(_userService.AddUserRole(id, roleId));
        }

        // N'autoriser l'accès qu'aux administrateurs
        [HttpDelete("{id}")]
        [Authorize(CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RolesData.ADMIN_ROLE)]
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
        [Authorize(CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdatePassword([FromBody] PasswordForm password)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!User.Equals(password.Id) && !User.IsInRole(RolesData.ADMIN_ROLE)) return Unauthorized();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_userService.UpdatePassword(password.Id, password.OldPassword, password.NewPassword))
                return BadRequest();

            return Ok(_userService.GetUser(password.Id));
        }
    }
}
