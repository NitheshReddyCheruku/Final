namespace GrowthPath.LearningAPI.Models
{
    public class CourseProgress
    {
        public int CourseProgressId { get; set; }
        public string EmployeeId { get; set; }
        public int CourseId { get; set; }
        public int ModulesCompleted { get; set; }
    }
}
