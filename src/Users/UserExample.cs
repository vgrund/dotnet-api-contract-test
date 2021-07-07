using System;
using Swashbuckle.AspNetCore.Filters;

namespace Users
{
    public class UserExample : IExamplesProvider<User>
    {
        public User GetExamples()
        {
            return new User()
            {
                Id = new Guid("ba8e6bc0-f02d-4f71-98cf-6f63b52434e0"),
                FirstName = "John",
                LastName = "Lennon",
                Email = "jl@email.com",
                Phone = "9999999999"
            };
        }
    }
}