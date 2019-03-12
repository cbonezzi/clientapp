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
    public class PostCommand : ICommand
    {
        public string Description => "1. Create User";

        public RequestModel GatherData()
        {
            Console.WriteLine("Enter 32 bit guid or leave blank");
            var guid = Console.ReadLine();

            Console.WriteLine("Enter expiration or leave blank");
            var expire = Console.ReadLine();

            var username = string.Empty;
            do
            {
                Console.WriteLine("Must enter a username");
                username = Console.ReadLine();
            } while (string.IsNullOrEmpty(username));

            return new RequestModel
            {
                UserId = string.IsNullOrEmpty(guid) ? string.Empty : guid,
                Expire = string.IsNullOrEmpty(expire) ? string.Empty : expire,
                Username = username
            };
        }
        public async Task<ResponseModel> Execute(RequestModel model)
        {
            var client = Client.CreateClient();
            var response = await Post(client, model);
            return response;
        }

        private static async Task<ResponseModel> Post(HttpClient client, RequestModel model)
        {
            ResponseModel rm = new ResponseModel();

            var guid = model.UserId;
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("Expire", model.Expire),
                new KeyValuePair<string,string>("Username", model.Username)
            });
            using (client)
            {
                var response = await client.PostAsync("api/guid/" + guid, content);
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
