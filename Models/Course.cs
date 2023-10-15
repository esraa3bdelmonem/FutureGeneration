using System.ComponentModel.DataAnnotations;

namespace Future_Generation.Models
{
    public enum CourseStatus
    {
        Started,
        NotStarted,
        Canceled
    }

    public class Course
    {
        public Course()
        {
            //SyllabusAttachment = new byte[0];
        }
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Course Name must be between 3 and 20 characters.")]
        public string CourseName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Cost is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive number.")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        public int Capacity { get; set; }
        public CourseStatus Status { get; set; }
        //public IFormFile SyllabusAttachmentFile { get; set; }
        //[DataType(DataType.Upload)]
        ////[FileExtensions(Extensions = "pdf", ErrorMessage = "Only PDF files are allowed.")]
        //[MaxFileSize(2 * 1024 * 1024, ErrorMessage = "Syllabus attachment size should not exceed 2 MB.")]
        public string SyllabusAttachment { get; set; } = string.Empty;

        //public byte[] SyllabusAttachment { get; set; }


        public ICollection<StudentCourses> StudentCourses { get; set; } = new HashSet<StudentCourses>();
    }
}
