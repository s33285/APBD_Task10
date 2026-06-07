using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class AssignCourseDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a course")]
        public int CourseId { get; set; }
    }
}
