using System;
using System.Collections.Generic;
using System.Linq;

namespace Users.Repository
{
    public class UsersRepository
    {
        protected List<User> users;

        public UsersRepository()
        {
            users = new List<User>(2){
                new User(){
                    Id = new Guid("ba8e6bc0-f02d-4f71-98cf-6f63b52434e0"),
                    FirstName = "John",
                    LastName = "Lennon",
                    Email = "jl@email.com",
                    Phone = "9999999999"
                },
                new User(){
                    Id = new Guid("ee4a8bd0-4792-472b-87a4-228ec2db84e0"),
                    FirstName = "Paul",
                    LastName = "McCartney",
                    Email = "pm@email.com",
                    Phone = "9999999999"
                }
            };
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