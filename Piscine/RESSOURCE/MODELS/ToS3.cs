


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.MODELS
{
    public class ToS3
    {
        public int id { get; set; }
        public string pathLandScape { get; set; }
        public string pathPortrait { get; set; }
        public DateTime createdAt { get; set; }
        public int ResultId { get; set; }
    }
}
