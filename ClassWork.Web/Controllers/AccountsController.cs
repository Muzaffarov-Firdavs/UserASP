using ClassWork.Service.DTOs.Users;
using ClassWork.Service.Interfaces;
using ClassWork.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassWork.Web.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        public AccountsController(IAuthService authService, IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> PostUserAsync(UserForCreationDto dto)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.CreateAsync(dto)
            });

        [HttpPost]
        [Route("generate-token")]
        public async Task<IActionResult> GenerateTokenAsync(string username, string password = null)
        {
            var token = await this.authService.GenerateTokenAsync(username, password);
            return Ok(new Response
            {
                Code = 200,
                Error = "Success",
                Data = token
            });
        }

    }
}
