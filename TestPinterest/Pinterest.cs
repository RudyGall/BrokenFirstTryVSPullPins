using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TestPinterest
{
    class Pinterest
    {
        public string AppIDSecret { get; set; }
        public string AppID { get; set; }
        public string AccessToken { get; set; }

        public async Task<IEnumerable<string>> GetPins(string url, string first_name, string last_name)
        {
            if (AccessToken == null)
            {
                AccessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.pinterest.com/v1/users/GcSocialmediatest/"));
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + AccessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            var serializer = new JavaScriptSerializer();
            dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            var enumerablePins = (json as IEnumerable<dynamic>);

            if (enumerablePins == null)
            {
                return null;
            }
            return enumerablePins.Select(pin => (string)(pin["data"].ToString()));
        }

        public async Task<string> GetAccessToken()
        {
            var httpClient = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, "https://api.pinterest.com/oauth2/token "); 
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.getpostman.com/oauth2/callback");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(AppID + ":" + AppIDSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            //HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);

            string json = await response.Content.ReadAsStringAsync();
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(json);
            return item["access_token"];
        }
    }
}