using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookTests.Dependencies
{
    public interface ILogger
    {
        void Log(String message);
        List<String> GetActions();
        int GetNumberOfCalls();
        static object GetObject { get; set; }
    }
}
