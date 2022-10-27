using Debugram.Data.Context;
using Debugram.Data.Contracts;
using Debugram.Entities.ModelDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.Data.Repositories
{
    public class UserRepository: Repository<User> , IUserRepository
    {
        public UserRepository(ApplicationDbContext context) :
            base(context)
        {
        }

        public User GetUserByEmail(string email)
        {
            var user = TableNoTracking.FirstOrDefault(n=>n.Email == email);
            return user;
        }
    }
}
