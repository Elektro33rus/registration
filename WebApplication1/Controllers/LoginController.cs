using AuthProject.Services;
using AuthProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthProject.Controllers
{
    [Route("api/Login")]
    public class LoginController : BaseController
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAndPasswordModel model)
        {
            var check = await _loginService.LoginAsync(model);
            if (!check) return GenerateForbiddenResponse();

            return Ok();
        }
    }
}
