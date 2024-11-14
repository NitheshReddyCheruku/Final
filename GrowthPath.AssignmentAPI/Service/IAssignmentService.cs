
using GRowthPath.AssignmentAPI.Models.DTO;

namespace GRowthPath.AssignmentAPI.Service
{
    public interface IAssignmentService
    {
        Task<bool> AssignCourse(string employeeId, int courseId);
        Task<bool> UpdateCourseProgress(string employeeId, int courseId, int modulesCompleted);


    }
}