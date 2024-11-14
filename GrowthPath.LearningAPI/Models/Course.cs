using System.ComponentModel.DataAnnotations;

namespace GrowthPath.LearningAPI.Models
{
    public class Course
    {

        [Key]
        public int CourseId { get; set; } // Primary key for the Course entity

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } // Title of the course

        [MaxLength(500)]
        public string Description { get; set; } // Brief description of the course content

        [Required]
        public DateTime EndDate { get; set; } // Date when the course was created
        public int TotalModules { get; set; }


    }
}
