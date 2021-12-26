using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterForm register)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            // TODO : Vérifier si l'email est disponible
        }
    }
}
