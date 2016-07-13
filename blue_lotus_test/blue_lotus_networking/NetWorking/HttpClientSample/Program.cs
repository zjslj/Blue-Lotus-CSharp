using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Net;


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
            //WebBrowser wb = new WebBrowser();
            //wb.Navigate("http://localhost:63325/");
            //wb.Print();
            //UriBuilder
            UriBuilder URIPage = new UriBuilder();
            URIPage.Scheme = "http";
            URIPage.Host = "http://192.168.5.16";
            URIPage.Port = 8090;
            URIPage.Path = "projects/FIST/repos/fixed-business/browse";
           // Uri MSPage = URIPage.Uri;
            Uri MSPage = new Uri("http://192.168.5.16:8090/projects/FIST/repos/fixed-business/browse?test=123");
            string Query = MSPage.Query;
            Console.WriteLine("Query:{0}", Query);

            string AbsolutePath = MSPage.AbsolutePath;
            Console.WriteLine("AbsolutePath:{0}", AbsolutePath);

            string Scheme = MSPage.Scheme;
            Console.WriteLine("Scheme:{0}", Scheme);

            int Port = MSPage.Port;
            Console.WriteLine("Port:{0}", Port);

            string Host = MSPage.Host;
            Console.WriteLine("Host:{0}", Host);

            bool IsDefaultPort = MSPage.IsDefaultPort;
            Console.WriteLine("IsDefaultPort:{0}", IsDefaultPort);

            //ip address
            IPAddress ipAddress = IPAddress.Parse("192.168.5.28");
            byte[] address = ipAddress.GetAddressBytes();
            string ipString = ipAddress.ToString();
            Console.WriteLine("ipAddress-byte:{0},string:{1}", address.Length, ipString);

            string loopback = IPAddress.Loopback.ToString();
            string broadcast = IPAddress.Broadcast.ToString();
            Console.WriteLine("loopback:{0}, broadcast:{1}", loopback, broadcast);

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
