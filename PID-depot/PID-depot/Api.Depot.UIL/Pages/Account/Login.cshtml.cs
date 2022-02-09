using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Api.Depot.UIL.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginForm Login { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUl = null)
        {
            if (returnUl is not null) ReturnUrl = returnUl;
        }

        public async Task OnPostAsync()
        {

        }
    }
}
