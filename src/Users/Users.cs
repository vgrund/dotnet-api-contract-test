using System.Collections.Generic;
using Newtonsoft.Json;

namespace Users
{
    public class Users
    {
        [JsonProperty("xpto")]
        public List<User> User { get; set; }
    }
}