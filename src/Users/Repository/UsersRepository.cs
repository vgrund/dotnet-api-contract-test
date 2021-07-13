using System;
using System.Collections.Generic;
using System.Linq;

namespace Users.Repository
{
    public class UsersRepository : IUsersRepository
    {
        protected List<User> users;

        public UsersRepository()
        {
            users = new List<User>();
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User GetById(Guid id)
        {
            return users.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}