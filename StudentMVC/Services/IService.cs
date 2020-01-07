using StudentMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMVC.Services
{
    public interface IService
    {
        void SendFeedback(Feedback feedback);
    }
}
