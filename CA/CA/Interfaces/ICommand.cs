using CA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.Interfaces
{
    //using command pattern since it is best suited for this types of menus.
    public interface ICommand
    {
        string Description { get; }
        RequestModel GatherData();
        Task<ResponseModel> Execute(RequestModel model);

    }
}
