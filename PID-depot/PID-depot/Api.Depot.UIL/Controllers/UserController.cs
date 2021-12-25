using Api.Depot.BLL.Dtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
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
    }
}
