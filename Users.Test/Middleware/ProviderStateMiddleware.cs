using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;
using Users.Repository;

namespace Users.Test.Middleware
{
    public class ProviderStateMiddleware
    {
        private const string ConsumerName = "API Users v1 - Release v1.0";
        private readonly RequestDelegate _next;
        private readonly IDictionary<string, Action> _providerStates;
        private readonly IUsersRepository _usersRepository;

        public ProviderStateMiddleware(RequestDelegate next, IUsersRepository usersrepository)
        {
            _usersRepository = usersrepository;
            _next = next;
            _providerStates = new Dictionary<string, Action>
            {
                {
                    "GetByIdUsers-404",
                    RemoveAllData
                }
            };
        }

        private void RemoveAllData()
        {
            ((FakeUsersRepository)_usersRepository).Users.Clear();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                await this.HandleProviderStatesRequestAsync(context);
                await context.Response.WriteAsync(String.Empty);
            }
            else
            {
                await this._next(context);
            }
        }

        private async Task HandleProviderStatesRequestAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
                context.Request.Body != null)
            {
                string jsonRequestBody = String.Empty;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    jsonRequestBody = await reader.ReadToEndAsync();
                }

                var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                //A null or empty provider state key must be handled
                if (providerState != null && !String.IsNullOrEmpty(providerState.State) &&
                    providerState.Consumer == ConsumerName)
                {
                    _providerStates.TryGetValue(providerState.State, out Action action);
                    if(action != null)
                    {
                        action.Invoke();
                    }
                }
            }
        }
    }
}