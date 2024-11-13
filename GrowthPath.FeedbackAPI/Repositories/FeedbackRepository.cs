using GrowthPath.FeedbackAPI.Data;
using GrowthPath.FeedbackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GrowthPath.FeedbackAPI.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackDbContext _context;

        public FeedbackRepository(FeedbackDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks() => await _context.Feedbacks.ToListAsync();

        public async Task<Feedback> GetFeedbackById(int feedbackId) => await _context.Feedbacks.FindAsync(feedbackId);

        public async Task<IEnumerable<Feedback>> GetFeedbacksByCourseId(int courseId) =>
            await _context.Feedbacks.Where(f => f.CourseId == courseId).ToListAsync();

        public async Task AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeedback(Feedback feedback)
        {
            _context.Entry(feedback).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedback(int feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
