namespace StudentMVC.Repository
{
    public class FeedbackRepoFactory
    {
        private static FeedbackRepo feedbackRepo;

        public static FeedbackRepo GetFeedbackRepo()
        {
            if(feedbackRepo == null)
            {
                feedbackRepo = new FeedbackRepo();
            }

            return feedbackRepo;
        }
    }
}
