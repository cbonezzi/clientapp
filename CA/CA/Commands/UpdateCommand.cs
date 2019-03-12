using CA.Interfaces;
using CA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CA.Commands
{
    public class UpdateCommand : ICommand
    {
        public string Description => "3. Update User";

        public async Task<ResponseModel> Execute(RequestModel model)
        {
            var client = Client.CreateClient();
            var response = await Put(client, model);
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

            Console.WriteLine("Enter expiration or leave blank");
            var expire = Console.ReadLine();

            Console.WriteLine("Enter a username or leave blank");
            var username = Console.ReadLine();

            return new RequestModel
            {
                UserId = string.IsNullOrEmpty(guid) ? string.Empty : guid,
                Expire = string.IsNullOrEmpty(expire) ? string.Empty : expire,
                Username = username
            };
        }
        private static async Task<ResponseModel> Put(HttpClient client, RequestModel model)
        {
            ResponseModel rm;

            var guid = model.UserId;
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("Expire", string.IsNullOrEmpty(model.Expire) ? string.Empty : model.Expire),
                new KeyValuePair<string,string>("Username", string.IsNullOrEmpty(model.Username) ? string.Empty : model.Username) 
            });
            using (client)
            {
                var response = await client.PutAsync("api/guid/" + guid, content);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    rm = await response.Content.ReadAsAsync<ResponseModel>();
                    return rm;
                }
                return null;
            }
        }
    }
}
