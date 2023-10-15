using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Future_Generation.Models
{
    public class StudentCourses
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Student")]
        //[Required(ErrorMessage = "The Student field is required.")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        //[Required(ErrorMessage = "The Course field is required.")]
        public int CourseId { get; set; }


        public Student Student { get; set; } = default!;
        public Course Course { get; set; } = default!;
    }
}
