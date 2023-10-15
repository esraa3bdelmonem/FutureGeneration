using Future_Generation.Common;
using Future_Generation.Repositories;

namespace Future_Generation.RepoServices
{
    public class StudentCoursesRepositoryService : IRepository<StudentCourses>
    {
        public ApplicationDbContext Context { get; }
        public StudentCoursesRepositoryService(ApplicationDbContext _Context)
        {
            Context = _Context;
        }
       public void Add(StudentCourses StudentCourses)
        {
            try
            {
                Context.StudentCourses.Add(StudentCourses);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Delete(int id)
        {
            var deletedStudentCourses = Context.StudentCourses.Where(sc => sc.Id == id).FirstOrDefault();
            if (deletedStudentCourses != null)
            {
                Context.StudentCourses.Remove(deletedStudentCourses);
                Context.SaveChanges();
            }
            else
            {
                Console.WriteLine("EmployeeDepartment not found");
            }
        }

        public List<StudentCourses> GetAll()
        {
            var StudentCourses = Context.StudentCourses
                .Include(c => c.Student)
                .Include(c => c.Course).ToList();

            return StudentCourses;
        }

        public StudentCourses GetById(int id)
        {
            var StudentCourses = Context.StudentCourses.Where(cs => cs.Id == id)
                .Include(c => c.Student)
                .Include(c => c.Course)
                .FirstOrDefault();
            if (StudentCourses == null)
            {
                Console.WriteLine("StudentCourses not found");
            }
            return StudentCourses;
        }

        public void Update(int id, StudentCourses StudentCourses)
        {
            var UpdatedStudentCourses = Context.StudentCourses.Where(sc => sc.Id == id).FirstOrDefault();
            if (UpdatedStudentCourses != null)
            {
                UpdatedStudentCourses.StudentId = StudentCourses.StudentId;
                UpdatedStudentCourses.CourseId = StudentCourses.CourseId;
                Context.SaveChanges();
            }
            else
            {
                Console.WriteLine("StudentCourses not found");
            }
        }

        public SearchResult<StudentCourses> GetAll(EntitySC sc)
        {
            throw new NotImplementedException();
        }
    }
}
