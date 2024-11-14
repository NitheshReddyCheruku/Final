using GrowthPath.LearningAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GrowthPath.LearningAPI.Data
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        //public DbSet<AssignedCourse> AssignedCourses { get; set; }
    }
}
