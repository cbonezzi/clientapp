using CA.Interfaces;
using CA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.Commands
{
    // exit command
    public class ExitCommand : ICommand
    {
        public string Description { get => "5. Exit"; }

        //execute command
        public async Task<ResponseModel> Execute(RequestModel model)
        {
            if(model == null)
            {
                Environment.Exit(0);
            }
            return null;
        }

        //gather data - not needed
        public RequestModel GatherData()
        {
            return null;
        }
    }
}
