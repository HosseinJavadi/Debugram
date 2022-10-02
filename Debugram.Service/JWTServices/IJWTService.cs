
namespace Debugram.Services.JWTServices
{
    public interface IJWTService
    {
        Task<string> Generate(dynamic user);

    }
}