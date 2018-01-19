using Newtonsoft.Json;
using RESSOURCE.MODELS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.PROVIDERS
{
    public class GPPD
    {
        #region CREDENTIALS
        string token = ConfigurationManager.AppSettings["URLTOKEN"];
        string urlAPI = ConfigurationManager.AppSettings["URLAPI"];
        public string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        #endregion

        #region GET

        // GET TOUTES LES CAMPAIGNS.
        public List<Campaign> GetAllCampaigns()
        {
            List<Campaign> CampaignEnAttente = new List<Campaign>();
            try
            {
                // create my web request.
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(urlAPI + "campaign/?status=true&sort=desc");
                // use default credentials.
                webReq.UseDefaultCredentials = true;
                webReq.Headers.Add("X-Auth-token", token);
                // sets the method as a get.
                webReq.Method = "GET";
                // performs the request and gets the response.
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                // prints out the status code.
                Console.WriteLine(webResp.StatusCode);
                // prints out the server
                Console.WriteLine(webResp.Server);
                // create a stream to the response.
                Stream answer = webResp.GetResponseStream();
                // read the stream.
                StreamReader _answer = new StreamReader(answer);
                // display the stream.
                var stringObject = _answer.ReadToEnd();
                // serialiser.
                CampaignEnAttente = JsonConvert.DeserializeObject<List<Campaign>>(stringObject);

                Console.WriteLine(_answer.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return CampaignEnAttente;
        }

        // GET TOUTES LES CAMPAIGNS.
        public TestCase GetTestCase()
        {
            TestCase casDeTestEnAttente = new TestCase();
            try
            {
                // create my web request.
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(urlAPI + "campaign/?status=true&sort=desc");
                // use default credentials.
                webReq.UseDefaultCredentials = true;
                webReq.Headers.Add("X-Auth-token", token);
                // sets the method as a get.
                webReq.Method = "GET";
                // performs the request and gets the response.
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                // prints out the status code.
                Console.WriteLine(webResp.StatusCode);
                // prints out the server
                Console.WriteLine(webResp.Server);
                // create a stream to the response.
                Stream answer = webResp.GetResponseStream();
                // read the stream.
                StreamReader _answer = new StreamReader(answer);
                // display the stream.
                var stringObject = _answer.ReadToEnd();
                string ignored = JsonConvert.SerializeObject(stringObject,
                                                                Formatting.Indented,
                                                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                // serialiser.
                casDeTestEnAttente = JsonConvert.DeserializeObject<TestCase>(ignored);

                Console.WriteLine(_answer.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return casDeTestEnAttente;
        }
        // GET TOUTES Les Configurations demandé.
        public List<MODELS.Configuration> AllRequestedConfigurationss()
        {
            List<MODELS.Configuration> ConfigurationEnAttente = new List<MODELS.Configuration>();
            try
            {
                // create my web request.
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(urlAPI + "configuration/?status=true&sort=desc");
                // use default credentials.
                webReq.UseDefaultCredentials = true;
                webReq.Headers.Add("X-Auth-token", token);
                // sets the method as a get.
                webReq.Method = "GET";
                // performs the request and gets the response.
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                // prints out the status code.
                Console.WriteLine(webResp.StatusCode);
                // prints out the server
                Console.WriteLine(webResp.Server);
                // create a stream to the response.
                Stream answer = webResp.GetResponseStream();
                // read the stream.
                StreamReader _answer = new StreamReader(answer);
                // display the stream.
                var stringObject = _answer.ReadToEnd();
                // serialiser.
                ConfigurationEnAttente = JsonConvert.DeserializeObject<List<MODELS.Configuration>>(stringObject);

                Console.WriteLine(_answer.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return ConfigurationEnAttente;
        }

        // GET Configuration par ID.
        public MODELS.Configuration ConfigurationById(string configurationId)
        {
            MODELS.Configuration _configuration = new MODELS.Configuration();
            try
            {
                // create my web request.
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(urlAPI + "configuration/" + configurationId);
                // use default credentials.
                webReq.UseDefaultCredentials = true;
                webReq.Headers.Add("X-Auth-token", token);
                // sets the method as a get.
                webReq.Method = "GET";
                // performs the request and gets the response.
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                // prints out the status code.
                Console.WriteLine(webResp.StatusCode);
                // prints out the server
                Console.WriteLine(webResp.Server);
                // create a stream to the response.
                Stream answer = webResp.GetResponseStream();
                // read the stream.
                StreamReader _answer = new StreamReader(answer);
                // display the stream.
                var stringObject = _answer.ReadToEnd();
                // serialiser.
                _configuration = JsonConvert.DeserializeObject<MODELS.Configuration>(stringObject);

                Console.WriteLine(_answer.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return _configuration;
        }

        // GET Data par ID.
        public Data DataById(string dataId)
        {
            Data _data = new Data();
            try
            {
                // create my web request.
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(urlAPI + "data/" + dataId);
                // use default credentials.
                webReq.UseDefaultCredentials = true;
                webReq.Headers.Add("X-Auth-token", token);
                // sets the method as a get.
                webReq.Method = "GET";
                // performs the request and gets the response.
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                // prints out the status code.
                Console.WriteLine(webResp.StatusCode);
                // prints out the server
                Console.WriteLine(webResp.Server);
                // create a stream to the response.
                Stream answer = webResp.GetResponseStream();
                // read the stream.
                StreamReader _answer = new StreamReader(answer);
                // display the stream.
                var stringObject = _answer.ReadToEnd();
                // serialiser.
                _data = JsonConvert.DeserializeObject<Data>(stringObject);

                Console.WriteLine(_answer.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return _data;
        }
        #endregion

        #region POST
        // POST RESULTATS TESTS
        public async Task PostResults(string dataID, string pTitle, string responseServer,
                                        string timeLapse, string pageFormat,
                                        string testOutcome, string lienScreenShotLandScape,
                                        string lienScreenShotPortrait, string unixTimeStampStart,
                                        string unixTimeStampEnd, string idConfig)
        {
            var client = new HttpClient();
            var values = new Dictionary<string, string>
                        {
                           // Titre de la page.
                           { "pTitle", pTitle},
                           // Reponse serveur de la page.
                           { "responseServer", responseServer.ToString() },
                           // Temps de chargement de la page.
                           { "timelapse", timeLapse.ToString() },
                           // Type de la page.
                           { "pageFormat", pageFormat.ToString() },
                           // Resultats.
                           { "outCome", testOutcome },
                           // Lien large.
                           { "landscapePath", lienScreenShotLandScape },
                           // Lien simple.
                           { "portaitPath", lienScreenShotPortrait },
                           // Date debut.
                           { "timestamp_start", unixTimeStampStart.ToString() },
                           // Date fin.
                           { "timestamp_end", unixTimeStampEnd.ToString() },
                        };
            client.DefaultRequestHeaders.Add("X-Auth-token", token);
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(urlAPI + "data/" + dataID + "/result/" + idConfig + "/", content);
            var responseString = await response.Content.ReadAsStringAsync();
        }
        #endregion

        #region PATCH
        // PATCH DATA STATUS
        public async Task PatchCampaignStatus(bool status, int campaignId)
        {
            var client = new HttpClient();
            {
                var _date = DateTime.Now;
                var values = new Dictionary<string, string>
                        {
                           { "status", status.ToString() }
                        };

                client.DefaultRequestHeaders.Add("X-Auth-token", token);

                var method = new HttpMethod("PATCH");

                var reqmsg = new HttpRequestMessage(method, urlAPI + "campaign/" + campaignId)
                {
                    Content = new FormUrlEncodedContent(values)
                };
                HttpResponseMessage response = await client.SendAsync(reqmsg);
            }
        }

        // GET UNE SEULE CAMPAGNE
        public Campaign GetCampaignWithID(string idCampaign)
        {
            Campaign CampaignEnAttente = new Campaign();
            try
            {
                // create my web request.
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(urlAPI + "campaign/" + idCampaign);
                // use default credentials.
                webReq.UseDefaultCredentials = true;
                webReq.Headers.Add("X-Auth-token", token);
                // sets the method as a get.
                webReq.Method = "GET";
                // performs the request and gets the response.
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                // prints out the status code.
                Console.WriteLine(webResp.StatusCode);
                // prints out the server
                Console.WriteLine(webResp.Server);
                // create a stream to the response.
                Stream answer = webResp.GetResponseStream();
                // read the stream.
                StreamReader _answer = new StreamReader(answer);
                // display the stream.
                var stringObject = _answer.ReadToEnd();
                // serialiser.
                CampaignEnAttente = JsonConvert.DeserializeObject<Campaign>(stringObject);

                Console.WriteLine(_answer.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CampaignEnAttente;
        }
        #endregion

        #region GET FROM FILE POUR MES TESTS
        public TestCase ChargerTestCase()
        {
            StreamReader r = new StreamReader(documents + @"\Source\Repos\automate_v2\RESSOURCE\CONFIGS\DEBUG\TC.json");
            string json = r.ReadToEnd();

            TestCase _tCase = JsonConvert.DeserializeObject<TestCase>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return _tCase;
        }
        #endregion
    }
}
