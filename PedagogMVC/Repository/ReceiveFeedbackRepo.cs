using PedagogMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedagogMVC.Repository
{
    public interface IFeedbackRepo
    {
        void CreateFeedback(ReceiveFeedback feedback);
        List<ReceiveFeedback> GetAllMyFeedbacks();

        ReceiveFeedback GetFeedbackById(Guid id);

        void DeleteFeedback(ReceiveFeedback feedback);

    }


    public class ReceiveFeedbackRepo : IFeedbackRepo
    {
        private readonly List<ReceiveFeedback> List;

        public ReceiveFeedbackRepo()
        {
            List = new List<ReceiveFeedback>();
            List.Add(new ReceiveFeedback
            {
                Id = Guid.NewGuid(),
                
                GoodFeedback = "Mi-a placut primul curs ",
                BadFeedback = "M-ar ajuta daca cursul ar contine mai multe exemple de cod"


            });

        }
        public void CreateFeedback(ReceiveFeedback feedback)
        {
            List.Add(feedback);
        }

        public void DeleteFeedback(ReceiveFeedback feedback)
        {
            List.Remove(feedback);
        }

        public List<ReceiveFeedback> GetAllMyFeedbacks()
        {
            return List;
        }

        public ReceiveFeedback GetFeedbackById(Guid id)
        {
            return List.FirstOrDefault(_ => _.Id == id);

        }
    }
}
