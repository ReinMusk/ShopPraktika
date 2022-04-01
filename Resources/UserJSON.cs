using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShopPraktika.Resources
{
    public class UserJSON
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class JsonObject
    {
        [JsonProperty("User")]
        public UserJSON[] User { get; set; }
    }
}
