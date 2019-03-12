using CA.Interfaces;
using CA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CA.Commands
{
    public class GetCommand : ICommand
    {
        public string Description => "2. Read User";

        public async Task<ResponseModel> Execute(RequestModel model)
        {
            var client = Client.CreateClient();
            var response = await Get(client, model);
            return response;
        }

        public RequestModel GatherData()
        {
            var guid = string.Empty;
            do
            {
                Console.WriteLine("Must Enter 32 bit guid");
                guid = Console.ReadLine();
            } while (string.IsNullOrEmpty(guid));

            return new RequestModel
            {
                UserId = string.IsNullOrEmpty(guid) ? string.Empty : guid
            };
        }
        private static async Task<ResponseModel> Get(HttpClient client, RequestModel model)
        {
            ResponseModel rm = new ResponseModel();
            var guid = model.UserId;
            using (client)
            {
                HttpResponseMessage response = await client.GetAsync("api/guid/" + guid);
                if (response.IsSuccessStatusCode)
                {
                    rm = await response.Content.ReadAsAsync<ResponseModel>();
                    return rm;
                }
            }
            rm.ValidationResults.Add(new ValidationResult("400 - Client Error"));
            return rm;
        }
    }
}
