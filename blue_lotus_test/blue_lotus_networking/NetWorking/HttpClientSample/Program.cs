using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace HttpClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("In main before call to GetData");
            //GetData();
            //Console.WriteLine("Back in main after call to GetData");
            //Process myProcess = new Process();
            //myProcess.StartInfo.FileName = "iexplore.exe";
            //myProcess.StartInfo.Arguments = "http://localhost:63325/";
            //myProcess.Start();
            WebBrowser wb = new WebBrowser();
            
            Console.ReadKey();
        }
        private static async void GetData()
        {
            HttpClient httpClient = new HttpClient(new MessageHandler("success"));
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            Console.WriteLine("Request Headers : ");
            EnumerateHeaders(httpClient.DefaultRequestHeaders);
            Console.WriteLine();

            HttpResponseMessage response = null;
            response = await httpClient.GetAsync("http://localhost:63325/");
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Resoponse Status Code : {0}, resopons:{1}", response.StatusCode, response.ReasonPhrase);
                string responseBodyAsText = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Received payload of {0} characters.", responseBodyAsText.Length);
                Console.WriteLine("Response Headers:");
                EnumerateHeaders(response.Headers);
            }
            else
            {
                Console.WriteLine("Resoponse Status Code : {0}, resopons:{1}", response.StatusCode, response.ReasonPhrase);
            }
        }
        private static void EnumerateHeaders(HttpHeaders headers)
        {
            foreach (var header in headers)
            {
                var value = "";
                foreach(var val in header.Value)
                {
                    value += val + " ";
                }
                Console.WriteLine("Header:{0}, Value:{1}", header.Key, value);
            }
            
        }
    }
    public class MessageHandler : HttpClientHandler
    {
        string displayMessage = "";
        public MessageHandler(string message)
        {
            displayMessage = message;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            Console.WriteLine("In DisplayMessageHandler" + displayMessage);
            if(displayMessage == "error")
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            else
            {
                return base.SendAsync(request, cancellationToken);
            }
            
           
        }
    }
}
