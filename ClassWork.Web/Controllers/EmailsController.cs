using ClassWork.Service.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ClassWork.Web.Controllers
{
    public class EmailsController : BaseController
    {
        private readonly EmailVerification emailVerification;
        public EmailsController(EmailVerification emailVerification)
        {
            this.emailVerification = emailVerification;
        }

        [HttpPost]
        public async Task<IActionResult> SendVerificationCodeAsync(string email)
            => Ok(await this.emailVerification.SendAsync(email));
    }
}
