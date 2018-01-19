using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.MODELS
{
    public class Configuration
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("deviceType")]
        public string device_type { get; set; }

        [JsonProperty("deviceModel")]
        public string device_model { get; set; }

        [JsonProperty("deviceMaker")]
        public object device_marker { get; set; }

        [JsonProperty("deviceOs")]
        public string device_os { get; set; }

        [JsonProperty("deviceOsVersion")]
        public string device_os_version { get; set; }

        [JsonProperty("deviceScreenSize")]
        public string screen_size { get; set; }

        [JsonProperty("deviceSerialNumber")]
        public string device_serial_number { get; set; }

        [JsonProperty("deviceNameTag")]
        public string device_name_tag { get; set; }

        [JsonProperty("deviceLabelling")]
        public string device_labelling;

        [JsonProperty("deviceLocation")]
        public string device_location { get; set; }

        [JsonProperty("deviceEnabled")]
        public string device_enabled { get; set; }

        [JsonProperty("deviceBrower")]
        public string device_browser { get; set; }

        [JsonProperty("createdAt")]
        public object createdAt { get; set; }

        [JsonProperty("port")]
        public string port { get; set; }

        [JsonProperty("connector")]
        public string connector { get; set; }

        [JsonProperty("inUse")]
        public string inUse { get; set; }

        [JsonProperty("Data")]
        public Data[] Data { get; set; }
    }
}