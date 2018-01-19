using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.MODELS
{
    public class DetailConfiguration
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("data")]
        public Data data { get; set; }

        [JsonProperty("configuration")]
        public Configuration configuration { get; set; }

        [JsonProperty("campaign")]
        public Campaign campaign { get; set; }
    }
}
