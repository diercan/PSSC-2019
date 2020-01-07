using CookBookTests.Dependencies;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookTests.TestDoubles
{
    public class LogSpy:ILogger
    {
        List<String> actions = new List<string>();
        public void Log(String message)
        {
            actions.Add(message);
        }


        public List<String> GetActions()
        {
            return actions;
        }

        public int GetNumberOfCalls()
        {
            return actions.Count;
        }

        public static object GetObject { get; set; }
    }
}
