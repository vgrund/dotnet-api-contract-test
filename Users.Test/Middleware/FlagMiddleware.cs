using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Moq;
using Newtonsoft.Json;
using NSubstitute;
using Users.Repository;

namespace Users.Test.Middleware
{
    public class FlagMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUsersRepository _usersRepository;

        public FlagMiddleware(RequestDelegate next, IUsersRepository usersrepository)
        {
            //_usersRepository = new UsersRepository();
            _usersRepository = usersrepository;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            
        }
    }
}