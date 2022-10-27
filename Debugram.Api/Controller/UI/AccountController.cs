using Debugram.CommonModel.InputModel.Account;
using Debugram.CommonModel.ViewModel.Account;
using Debugram.Data.Service.IService.Account;
using Debugram.Services.JWTServices;
using Debugram.WebFramework.Principles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Debugram.Api.Controller.UI
{
    [Route("api/Account/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJWTService _jWTService;

        public AccountController(IAccountService accountService, IJWTService jWTService)
        {
            _accountService = accountService;
            _jWTService = jWTService;
        }
        [HttpPost]
        public async Task<ApiResult<LoginViewModel>> LoginUser(RegisterInputModel param, CancellationToken cancellationToken)
        {
            var user = await Task.FromResult(_accountService.UserLogin(param)).WaitAsync(cancellationToken);

            var token = await Task.FromResult(_jWTService.Generate(user)).WaitAsync(cancellationToken);

            return new LoginViewModel()
            {
                User = user,
                Token = token
            };
        }
    }
}
