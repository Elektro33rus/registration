using System.Threading.Tasks;
using AuthProject.Services;
using Microsoft.AspNetCore.Mvc;
using AuthProject.ViewModels;

namespace AuthProject.Controllers
{
    [Route("api/User")]
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            return await GetResultAsync(() => _userService.CreateAsync(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UserUpdateModel model)
        {
            return await GetResultAsync(() => _userService.UpdateAsync(id, model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return await GetResultAsync(() => _userService.GetByIdAsync(id));
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ForgetPassword(ResetPasswordDtoModel model)
        {
            var checkEmail = await _userService.ResetPasswordAsync(model);
            if (!checkEmail) return NotFound();

            return Ok();
        }
    }
}