using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.MODELS
{
    public class Data
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("start")]
        public string start { get; set; }

        [JsonProperty("end")]
        public string end { get; set; }

        [JsonProperty("status")]
        public bool status { get; set; }

        [JsonProperty("dataInput")]
        public string dataInput { get; set; }

        [JsonProperty("dataOutput")]
        public string dataOutput { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        public List<object> Result { get; set; }
    }
}
