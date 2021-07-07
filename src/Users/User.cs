using System;

namespace Users
{
    /// <summary>
    /// The name of the product
    /// </summary>
    /// <example>{'ba8e6bc0-f02d-4f71-98cf-6f63b52434e0','John','Lennon','jl@email.com','9999999999'}</example>
    public class User
    {
       
        public Guid Id { get; set; }

        
        public string FirstName { get; set; }

      
        public string LastName { get; set; }

       
        public string Email { get; set; }

       
        public string Phone { get; set; }
    }
}
