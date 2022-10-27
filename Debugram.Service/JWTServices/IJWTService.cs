
using Debugram.CommonModel.ViewModel;

namespace Debugram.Services.JWTServices
{
    public interface IJWTService
    {
        string Generate(UserViewModel user);

    }
}