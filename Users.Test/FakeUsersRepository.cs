using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Repository;

namespace Users.Test
{
    public class FakeUsersRepository : IUsersRepository
    {
        public List<User> Users { get; set; }

        public FakeUsersRepository()
        {
            Users = new List<User>();
            //Users = new List<User>(2){
            //    new User(){
            //        Id = new Guid("ba8e6bc0-f02d-4f71-98cf-6f63b52434e0"),
            //        FirstName = "John",
            //        LastName = "Lennon",
            //        Email = "jl@email.com",
            //        Phone = "9999999999"
            //    },
            //    new User(){
            //        Id = new Guid("ee4a8bd0-4792-472b-87a4-228ec2db84e0"),
            //        FirstName = "Paul",
            //        LastName = "McCartney",
            //        Email = "pm@email.com",
            //        Phone = "9999999999"
            //    }
            //};
        }

        public List<User> GetAll()
        {
            return Users;
        }

        public User GetById(Guid id)
        {
            //return null;
            return Users.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}
