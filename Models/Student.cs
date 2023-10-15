using System.ComponentModel.DataAnnotations;

namespace Future_Generation.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Student Name is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Student Name must be between 3 and 20 characters.")]

        public string Name { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "start with 010 | 011 | 012 | 015 and max 11 Diget")]
        [MaxLength(11)]
        public string Telephone { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Address must not exceed 250 characters.")]
        public string Address { get; set; } = string.Empty;


        public ICollection<StudentCourses> StudentCourses { get; set; } = new HashSet<StudentCourses>();
    }
}
