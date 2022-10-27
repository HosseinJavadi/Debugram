using Debugram.Data.Repositories;
using Debugram.Entities.ModelDbContext;

namespace Debugram.Data.Contracts
{
    public interface IUserRepository:IRepository<User>
    {
        User GetUserByEmail(string email);
    }
}
