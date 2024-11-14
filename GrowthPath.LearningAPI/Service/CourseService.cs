using GrowthPath.LearningAPI.Data;
using GrowthPath.LearningAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GrowthPath.LearningAPI.Service
{
    public class CourseService : ICourseService
    {
        private readonly CourseDbContext _context;

        public CourseService(CourseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.CourseId);
            if (existingCourse == null) return false;

            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.EndDate = course.EndDate;
            existingCourse.TotalModules = course.TotalModules; // Update the total modules

            _context.Courses.Update(existingCourse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
