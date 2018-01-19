using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.MODELS
{
    public class Result
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime finishedAt { get; set; }
        public string dataID { get; set; }
        public string title { get; set; }
        public int responseServer { get; set; }
        public long timelapse { get; set; }
        public string pageFormat { get; set; }
        public string testOutcome { get; set; }
        public string lienScreenShotLandScape { get; set; }
        public string lienScreenShotPortrait { get; set; }
        public Int32 unixTimeStampStart { get; set; }
        public Int32 unixTimeStampEnd { get; set; }

        public void FillUpResult(string dataID, string title, DateTime _dateStartTest, DateTime _dateEndTest,
                                 string serverResponse, long auTemps, string resultOutcome,
                                 string lienScreenShotLandScape, string lienScreenShotPortrait,
                                 Int32 unixTimeStampStart, Int32 unixTimeStampEnd)
        {
            this.dataID = dataID;
            this.title = title;
            this.createdAt = _dateStartTest;
            this.finishedAt = _dateEndTest;
            this.responseServer = Int32.Parse(serverResponse);
            this.timelapse = auTemps;
            this.testOutcome = resultOutcome;
            this.lienScreenShotLandScape = lienScreenShotLandScape;
            this.lienScreenShotPortrait = lienScreenShotPortrait;
            this.unixTimeStampStart = unixTimeStampStart;
            this.unixTimeStampEnd = unixTimeStampEnd;
        }
    }
}
