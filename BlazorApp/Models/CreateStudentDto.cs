using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Index number is required.")]
        public string IndexNumber { get; set; } = "";

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = "";

        [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8.")]
        public int Semester { get; set; } = 1;
    }
}
