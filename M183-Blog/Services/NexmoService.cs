using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;

namespace M183_Blog.Services
{
    public class NexmoService
    {
        private static readonly HttpClient client = new HttpClient();
        private string apiKey;
        private string apiSecret;
        private string apiEndpoint;

        public NexmoService()
        {
            apiKey = WebConfigurationManager.AppSettings["Nexmo_Api_Key"];
            apiSecret = WebConfigurationManager.AppSettings["Nexmo_Api_Secret"];
            apiEndpoint = WebConfigurationManager.AppSettings["Nexmo_Api_Endpoint"];
        }


        private string GetPostData(int secret, string phonenumber)
        {
            string postData = "api_key=" + apiKey;
            postData += "&api_secret=" + apiSecret;
            postData += "&to=" + phonenumber;
            postData += "&text=\"\"Whats up? Here is your SECRET to login: " + secret + ". DO NOT SHARE THIS CODE. \"\"";
            postData += "&from=\"\"Yves and Livios super awesome Blog\"\"";

            return postData;
        }

        public string SendSMS(int secret, string phonenumber)
        {

            var request = (HttpWebRequest)WebRequest.Create(apiEndpoint);

            var data = Encoding.ASCII.GetBytes(GetPostData(secret, phonenumber));

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}