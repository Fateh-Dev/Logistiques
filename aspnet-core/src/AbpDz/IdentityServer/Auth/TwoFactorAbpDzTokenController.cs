using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AbpDz.IdentityServer
{
    public class SetCodeDto
    {
        public string UserName { get; set; }
        public bool ResetPassword { get; set; }
    }

    public class ResetPasswordDto
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public string Code { get; set; }
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TwoFactorAbpDzTokenController : Controller
    {
        public TwoFactorAbpDzTokenController(TwoFactorAbpTokenGenerator TwoFactorAbpTokenGenerator)
        {
            this.TwoFactorAbpTokenGenerator = TwoFactorAbpTokenGenerator;
        }

        public TwoFactorAbpTokenGenerator TwoFactorAbpTokenGenerator { get; }

        [HttpPost("/connect/token_code")]
        public async Task<ActionResult> SetCode([FromBody] SetCodeDto body)
        {
            if (await TwoFactorAbpTokenGenerator.SendToken(body.UserName, body.ResetPassword))
            {
                return Ok(true);
            }
            return NotFound("AbpAccount::DefaultErrorMessage");
        }


        [HttpPost("/connect/reset_password")]
        public async Task<ActionResult> RestPassword([FromBody] ResetPasswordDto body)
        {
            if (body.Code.Length < 10)
            {
                return NotFound("AbpDz::InvalidVerificationCode");
            }
            var r = await this.TwoFactorAbpTokenGenerator.ResetPassword(body.UserName, body.NewPassword, body.Code);
            if (r != null)
            {
                return NotFound(r);
            }
            return Ok(true);
        }
    }
}
