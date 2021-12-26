using Api.Depot.BLL.Dtos;
using Api.Depot.BLL.Dtos.UserDtos;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        [Route("Users/{id}")]
        public IActionResult GetUser(Guid userId)
        {
            UserDto userFromService = _userService.GetUser(userId);
            if (userFromService is null) return NotFound(userId);
            return Ok(new UserModel(userFromService));
        }

        [HttpGet]
        [Route("Users")]
        public IActionResult GetUsers()
        {
            IEnumerable<UserDto> usersFromService = _userService.GetUsers();
            return Ok(usersFromService.Select(u => new UserModel(u)));
        }

        [HttpPost]
        [Route("Users")]
        public IActionResult UpdateUser([FromBody] UserForm user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            UserDto updatedUser = _userService.UpdateUser(user.MapBLL());
            if (updatedUser is null) return BadRequest(user);
            return Ok(new UserModel(updatedUser));
        }

        // N'autoriser l'accès qu'aux administrateur
        [HttpPost]
        [Route("Users/{userId}/Roles/{roleId}")]
        public IActionResult AddUserRole(Guid userId, Guid roleId)
        {
            return Ok();
        }

        // N'autoriser l'accès qu'aux administrateurs
        [HttpDelete]
        [Route("Users/{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            UserDto userFromRepo = _userService.GetUser(id);
            if (userFromRepo is null) return NotFound(id);
            return Ok(_userService.DeleteUser(id));
        }
    }
}
