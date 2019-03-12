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
    // delete command class
    public class DeleteCommand : ICommand
    {
        public string Description => "4. Delete User";
        
        //gather data
        public RequestModel GatherData()
        {
            Console.WriteLine("Enter 32 bit guid to delete user");
            var guid = Console.ReadLine();
            return new RequestModel
            {
                UserId = string.IsNullOrEmpty(guid) ? string.Empty : guid
            };
        }

        //execute command
        public async Task<ResponseModel> Execute(RequestModel model)
        {
            var client = Client.CreateClient();
            var response = await Delete(client, model);
            return response;
        }

        //private methods as they are different for each command.
        private static async Task<ResponseModel> Delete(HttpClient client, RequestModel model)
        {
            ResponseModel rm = new ResponseModel();
            using (client)
            {
                HttpResponseMessage response = await client.DeleteAsync("api/guid/" + model.UserId);
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
