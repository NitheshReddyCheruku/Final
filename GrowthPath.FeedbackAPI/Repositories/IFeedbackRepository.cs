using GrowthPath.FeedbackAPI.Models;

namespace GrowthPath.FeedbackAPI.Repositories
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<Feedback> GetFeedbackById(int feedbackId);
        Task<IEnumerable<Feedback>> GetFeedbacksByCourseId(int courseId);
        Task AddFeedback(Feedback feedback);
        Task UpdateFeedback(Feedback feedback);
        Task DeleteFeedback(int feedbackId);
    }
}
