using Newtonsoft.Json;
using RESSOURCE.MODELS.FUNCTIONAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.MODELS
{
    public class TestCase
    {
        [JsonProperty("id")]
        public string id;
        [JsonProperty("name")]
        public string name;
        [JsonProperty("action")]
        public ActionEvent[] Action { get; set; }
    }
}
