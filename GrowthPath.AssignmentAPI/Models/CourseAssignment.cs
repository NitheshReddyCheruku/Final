using System.ComponentModel.DataAnnotations;

namespace GRowthPath.AssignmentAPI.Models
{
    public class CourseAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [Required]
        public string EmployeeId { get; set; }  // UserId from Auth API with role Employee

        [Required]
        public int CourseId { get; set; }       // CourseId from Learning API

        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
        public int ModulesCompleted { get; set; }
    }
}
