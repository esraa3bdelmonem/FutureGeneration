namespace Future_Generation.Repositories
{
    public interface ICourseRepository 
    {
        void Add(Course entity, IFormFile syllabusAttachmentFile, IWebHostEnvironment webHostEnvironment);
        void Update(int id, Course Course, IFormFile? updatedSyllabusAttachmentFile, IWebHostEnvironment webHostEnvironment);
        List<Course> GetAll();
        Course GetById(int id);
      
        void Delete(int id);

    }
}
