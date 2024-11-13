using GrowthPath.FeedbackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GrowthPath.FeedbackAPI.Data
{
    public class FeedbackDbContext : DbContext
    {
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options) { }

        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
