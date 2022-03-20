using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly int ElementsPerPage = 10;

        public int ActualPage { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
        public bool HasPrevious { get; set; } = true;
        public bool HasNext { get; set; } = true;

        public UsersModel(ILogger<UsersModel> logger, IUserService userService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        public void OnGet(int p = 1)
        {
            ActualPage = p;

            if (p == 1) HasPrevious = false;

            var users = _userService.GetUsers();
            if (p * ElementsPerPage >= users.Count()) HasNext = false;

            Users = users.Skip(p - 1 * ElementsPerPage).Take(ElementsPerPage).Select(u => u.MapFromBLL());
        }
    }
}
