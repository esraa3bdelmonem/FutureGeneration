using Future_Generation.Common;
using Future_Generation.Repositories;

namespace Future_Generation.RepoServices
{
    public class StudentRepositoryService : IRepository<Student>
    {
        public ApplicationDbContext Context { get; }
        public StudentRepositoryService(ApplicationDbContext _Context)
        {
            Context = _Context;
        }
        public SearchResult<Student> GetAll(EntitySC sc)
        {
            throw new NotImplementedException();
        }

        public void Add(Student Student)
        {
            try
            {
                Context.Students.Add(Student);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Delete(int id)
        {
            var deletedStudent = Context.Students.Where(e => e.Id == id).FirstOrDefault();
            if (deletedStudent != null)
            {
                Context.Students.Remove(deletedStudent);
                Context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Student not found");
            }
        }

        public List<Student> GetAll()
        {
            var Students = Context.Students
               .Include(c => c.StudentCourses)
               .ThenInclude(c => c.Course).ToList();
            return Students;
        }

        public Student GetById(int id)
        {
            var Student = Context.Students.Where(e => e.Id == id)
             .Include(c => c.StudentCourses)
             .ThenInclude(c => c.Course).FirstOrDefault();
            if (Student == null)
            {
                Console.WriteLine("Student not found");
            }
            return Student;
        }

         public void Update(int id, Student Student)
        {
            var UpdatedStudent = Context.Students.Where(e => e.Id == id).FirstOrDefault();
            if (UpdatedStudent != null)
            {
                UpdatedStudent.Name = Student.Name;
                UpdatedStudent.Email = Student.Email;
                UpdatedStudent.Telephone = Student.Telephone;
                UpdatedStudent.Address = Student.Address;
                Context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Employee not found");
            }
        }
    }
}
