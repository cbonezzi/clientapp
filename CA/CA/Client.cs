using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CA
{
    public static class Client
    {
        /// <summary>
        /// create http client 
        /// </summary>
        /// <returns></returns>
        public static HttpClient CreateClient()
        {
            HttpClient cons = new HttpClient();

            cons.BaseAddress = new Uri("http://localhost:51947/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return cons;
        }
    }
}
