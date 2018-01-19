using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.MODELS.FUNCTIONAL
{
    public class ActionEvent
    {
        [JsonProperty("actionName")]
        public string actionName { get; set; }

        [JsonProperty("actionType")]
        public string actionType { get; set; }

        [JsonProperty("elementPropertyType")]
        public string elementPropertyType { get; set; }

        [JsonProperty("elementPropertyValue")]
        public string elementPropertyValue { get; set; }

        [JsonProperty("elementValue")]
        public string elementValue { get; set; }
    }
}

