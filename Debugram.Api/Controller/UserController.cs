using Debugram.WebFramework.Principles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Debugram.Api.Controller
{
    [Route("api/User/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public async Task<ApiResult<string>> GetName()
        {
            return "Hossein";
        }
    }
}
