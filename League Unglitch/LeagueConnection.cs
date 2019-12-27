using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace League_Unglitch
{
    /**
     * Class that manages a connection with the LCU. This will automatically connect to the League
     * client once it starts, and exposes a simple API for listening to requests, making requests
     * and lifetime events.
     */
    class LeagueConnection
    {
        private static HttpClient HTTP_CLIENT;
        private Tuple<Process, string, string> processInfo;
        private bool connected;

        /**
         * Creates a new LeagueConnection instance. This will immediately start trying
         * to connect to League.
         */
        public LeagueConnection()
        {
            try
            {
                HTTP_CLIENT = new HttpClient(new HttpClientHandler()
                {
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,
                    ServerCertificateCustomValidationCallback = (a, b, c, d) => true
                });
            }
            catch
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                HTTP_CLIENT = new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (a, b, c, d) => true
                });
            }
        }

        /**
         * Tries to connect to a currently running league process. Called
         * by the connection timer every 5 seconds.
         */
        private void TryConnect()
        {
            try
            {
                // Check league status, abort if league is not running.
                var status = LeagueUtils.GetLeagueStatus();
                if (status == null) return;

                // Set the password and base address for our httpclient so we don't have to specify it every time.
                var byteArray = Encoding.ASCII.GetBytes("riot:" + status.Item2);
                HTTP_CLIENT.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                

                // Update local state.
                processInfo = status;
                connected = true;
            }
            catch (Exception e)
            {
                processInfo = null;
            }
        }

        /**
         * Performs a GET request on the specified URL.
         */
        public async Task<dynamic> Get(string url)
        {
            TryConnect();
            if (!connected) throw new InvalidOperationException("Not connected to LCU");

            var res = await HTTP_CLIENT.GetAsync("https://127.0.0.1:" + processInfo.Item3 + url);
            var stringContent = await res.Content.ReadAsStringAsync();

            if (res.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            return SimpleJson.DeserializeObject(stringContent);
        }
        
        public async Task Post(string url, string body)
        {
            TryConnect();
            if (!connected) throw new InvalidOperationException("Not connected to LCU");

            await HTTP_CLIENT.PostAsync("https://127.0.0.1:" + processInfo.Item3 + url, new StringContent(body, Encoding.UTF8, "application/json"));
        }

        /**
         * Performs a PUT request on the specified URL with the specified body.
         */
        public async Task Put(string url, string body)
        {
            TryConnect();
            if (!connected) throw new InvalidOperationException("Not connected to LCU");

            await HTTP_CLIENT.PutAsync("https://127.0.0.1:" + processInfo.Item3 + url, new StringContent(body, Encoding.UTF8, "application/json"));
        }

        /**
         * Performs a request with the specified method on the specified URL with the specified body.
         */
        public Task<HttpResponseMessage> Request(string method, string url, string body)
        {
            TryConnect();
            if (!connected) throw new InvalidOperationException("Not connected to LCU");

            return HTTP_CLIENT.SendAsync(new HttpRequestMessage(new HttpMethod(method), "https://127.0.0.1:" + processInfo.Item3 + url)
            {
                Content = body == null ? null : new StringContent(body, Encoding.UTF8, "application/json")
            });
        }
    }
}