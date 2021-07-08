using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Users.Repository;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersRepository _repository;

        public UsersController(ILogger<UsersController> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _repository = usersRepository;
        }

        [HttpGet]
        [SwaggerOperation(
             OperationId = "GetAllUsers",
             Summary = "Obtêm todos os users",
             Description = "Este recurso lista todos os users cadastrados",
             Tags = new[] { "Users" }
         )]
        [SwaggerResponse(StatusCodes.Status200OK, "The list of users", typeof(Users))]
        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        /// <param name="userId" example="ba8e6bc0-f02d-4f71-98cf-6f63b52434e0">User Id</param>
        [HttpGet("{userId}")]
        [SwaggerOperation(
            OperationId = "GetByIdUsers",
            Summary = "Obtêm um user pelo seu Id",
            Description = "Este recurso apresenta o user",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "An user", typeof(User))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", typeof(void))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserExample))]
        public ActionResult GetById(Guid userId)
        {
            var user = _repository.GetById(userId);
            using StreamWriter file = new("WriteLines2.txt", append: true);
            file.WriteLine(JsonSerializer.Serialize(user));

            //return null;
            //return Ok(user);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [SwaggerOperation(
            OperationId = "CreateUsers",
            Summary = "Cria um user",
            Description = "Este recurso cria um user",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "An user", typeof(User))]
        public ActionResult Post([FromBody] User user)
        {
            return Created("", user);
        }
    }
}
