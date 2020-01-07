using MassTransit;

using StudentMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMVC.Repository
{

    public interface IFeedbackRepo
    {
        void CreateFeedback(Feedback feedback);
        List<Feedback> GetAllMyFeedbacks();

        Feedback GetFeedbackById(Guid id);

        void DeleteFeedback(Feedback feedback);

    }
    public class FeedbackRepo : IFeedbackRepo
    {
        private readonly List<Feedback> List;
       // private readonly IBus bus;

        public FeedbackRepo () 
        {
            List = new List<Feedback>();

            //var messageObject = new Feedback
            //{
            //    Id = Guid.NewGuid(),
            //    Profesor = ProfesorList.Albert_Moza,
            //    GoodFeedback = "Mi-a placut primul curs ",
            //    BadFeedback = "M-ar ajuta daca cursul ar contine mai multe exemple de cod"

            //};

            //List.Add(messageObject);

           

            //this.bus = bus;
            

            //bus.Publish<Feedback>(messageObject);

            




        }


        public void CreateFeedback(Feedback feedback)
        {

            List.Add(feedback);



        }

        public void DeleteFeedback(Feedback feedback)
        {
            List.Remove(feedback);

        }

        public List<Feedback> GetAllMyFeedbacks()
        {
            return List;
        }

        public Feedback GetFeedbackById(Guid id)
        {
            return List.FirstOrDefault(_ => _.Id == id);
        }
    }
}
