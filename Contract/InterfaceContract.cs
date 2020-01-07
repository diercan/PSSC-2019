using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMVC.Contract
{
    public class InterfaceContract
    {
        string Id { get; }
        string CustomerEmail { get; set; }
        string Message { get; set; }
    }
}
