using ClassWork.Service.DTOs.Users;
using ClassWork.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClassWork.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> PutUserAsync(UserForUpdateDto dto)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.UpdateAsync(dto)
            });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteUserASync(long id)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.DeleteAsync(id)
            });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetUserAsync(long id)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.RetriewAsync(id)
            });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllUsersAsync()
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.RetriewAllAsync()
            });


    }
}
