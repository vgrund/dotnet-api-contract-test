using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Filters;

namespace Users
{
    public class UsersExample : IExamplesProvider<IEnumerable<User>>
    {
        public IEnumerable<User> GetExamples()
        {
            return new List<User>(2){
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
    }
}