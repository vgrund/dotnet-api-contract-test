using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Repository
{
    public interface IUsersRepository
    {
        public List<User> GetAll();
        public User GetById(Guid id);
    }
}
