using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.MODELS
{
    public class Campaign
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("createdAt")]
        public string createdAt { get; set; }

        [JsonProperty("status")]
        public bool status { get; set; }

        [JsonProperty("finishAt")]
        public object finishAt { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("Configuration")]
        public Configuration[] Configuration { get; set; }
    }
}
