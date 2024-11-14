using GRowthPath.AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GRowthPath.AssignmentAPI.Data
{
    public class AssignmentDbContext:DbContext
    {
        public AssignmentDbContext(DbContextOptions<AssignmentDbContext> options) : base(options) { }

        public DbSet<CourseAssignment> CourseAssignments { get; set; }

    }
}
