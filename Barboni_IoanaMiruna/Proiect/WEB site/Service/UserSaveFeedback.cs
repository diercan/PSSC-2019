using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;

namespace LegumeDeBelint.Service
{
    public class UserSaveFeedback
    {
        internal List<User> failedList;

        public int successCount { get; internal set; }
        public List<User> FailedList { get; set; }
        public object FailedCount { get; internal set; }
    }
}