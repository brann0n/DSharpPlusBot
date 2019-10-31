using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpPlusBot.Entities
{
    public class Config
    {
        [JsonProperty("token")]
        public string Token { get; private set; } // not modifyable after loading

        [JsonProperty("commandprefix")]
        public string CommandPrefix { get; private set; } // not modifyable after loading
    }
}
