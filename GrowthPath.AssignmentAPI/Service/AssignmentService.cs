using GRowthPath.AssignmentAPI.Data;
using GRowthPath.AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GRowthPath.AssignmentAPI.Service
{
    public class AssignmentService : IAssignmentService
    {
        private readonly AssignmentDbContext _context;

        public AssignmentService(AssignmentDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AssignCourse(string employeeId, int courseId)
        {
            // Check if the course is already assigned to the employee
            var existingAssignment = await _context.CourseAssignments
.AnyAsync(a => a.EmployeeId == employeeId && a.CourseId == courseId);
            if (existingAssignment)
            {
                return false;  // Already assigned
            }

            // Assign the course to the employee
            var assignment = new CourseAssignment
            {
                EmployeeId = employeeId,
                CourseId = courseId
            };

            _context.CourseAssignments.Add(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCourseProgress(string employeeId, int courseId, int modulesCompleted)
        {
            var assignment = await _context.CourseAssignments
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.CourseId == courseId);

            if (assignment == null) return false;

            // You may need to fetch the course's TotalModules from the Learning API
            int totalModules = 4; // Placeholder value; replace with API call to Learning API

            if (modulesCompleted > totalModules) return false;

            assignment.ModulesCompleted = modulesCompleted;
            await _context.SaveChangesAsync();
            return true;
        }
    
}
}
