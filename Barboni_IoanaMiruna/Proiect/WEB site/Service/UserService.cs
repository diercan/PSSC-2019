using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace LegumeDeBelint.Service
{
    public class UserService
    {
        public object V { get; private set; }

        internal UserSaveFeedback InsertUsers(List<User> userList)
        {
            List<User> failList = new List<User>();
            foreach (User user in userList)
            {
                failList.Add(user);
                Thread.Sleep(1000);//fake waiting 
            }

            UserSaveFeedback saveFeedback = new UserSaveFeedback();
            saveFeedback.successCount = 1;
            saveFeedback.FailedCount = V;
            saveFeedback.failedList = failList;//Add fake failed Items

            return saveFeedback;
        }
    }
}
