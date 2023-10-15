using Future_Generation.Common;
using Future_Generation.Models;
using Future_Generation.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Future_Generation.Controllers
{
    public class StudentController : Controller
    {
        public ICourseRepository CourseRepository { get; }
        public IRepository<Student> StudentRepository { get; }
        public IRepository<StudentCourses> StudentCoursesRepository { get; }

        public StudentController(ICourseRepository _CourseRepository, IRepository<Student> _StudentRepository, IRepository<StudentCourses> _StudentCoursesRepository)
        {
            CourseRepository = _CourseRepository;
            StudentRepository = _StudentRepository;
            StudentCoursesRepository = _StudentCoursesRepository;
        }

        public ActionResult Index()
        {
            return View("Index", StudentRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            string Name = "";
            string Telephone = "";
            string Email = "";
            string Address = "";
            var Students = StudentRepository.GetAll();
            if (!string.IsNullOrEmpty(collection["name"]))
            {
                Name = collection["name"];
            }

            if (!string.IsNullOrEmpty(collection["telephone"]))
            {
                Telephone = collection["telephone"];
            }
            if (!string.IsNullOrEmpty(collection["email"]))
            {
                Email = collection["email"];
            }
            if (!string.IsNullOrEmpty(collection["address"]))
            {
                Address = collection["address"];
            }

            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Telephone)&& !string.IsNullOrEmpty(Email)&& !string.IsNullOrEmpty(Address))
            {
                return View(Students.Where(e => e.Name.Contains(Name) && e.Telephone.Contains(Telephone) && e.Email.Contains(Email)&& e.Address.Contains(Address)));
            }
            else if (!string.IsNullOrEmpty(Name))
            {
                return View(Students.Where(e => e.Name.Contains(Name)));
            }
            else if (!string.IsNullOrEmpty(Telephone))
            {
                return View(Students.Where(e => e.Telephone.Contains(Telephone)));
            }
            else if (!string.IsNullOrEmpty(Email))
            {
                return View(Students.Where(e => e.Email.Contains(Email)));
            }
            else if (!string.IsNullOrEmpty(Address))
            {
                return View(Students.Where(e => e.Address.Contains(Address)));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            ViewBag.AllCrss = StudentCoursesRepository.GetAll()
                .Where(e => e.StudentId == id).Select(c => c.Course.CourseName).ToList();
            return View("Details", StudentRepository.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student Student)
        {
            var Telephone = StudentRepository.GetAll().Any(c => c.Telephone == Student.Telephone);
            if (Telephone)
            {
                ModelState.AddModelError("Telephone", "Already Exist Telephone");
            }
            //var Name = StudentRepository.GetAll().Any(c => c.Name == Student.Name);
            //if (Name)
            //{
            //    ModelState.AddModelError("Name", "Already Exist Name");
            //}
            var Email = StudentRepository.GetAll().Any(c => c.Email == Student.Email);
            if (Email)
            {
                ModelState.AddModelError("Email", "Already Exist Email ");
            } 
            //var Address = StudentRepository.GetAll().Any(c => c.Address == Student.Address);
            //if (Address)
            //{
            //    ModelState.AddModelError("Address", "Already Exist Address ");
            //}

            if (ModelState.IsValid)
            {
                StudentRepository.Add(Student);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(StudentRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Student Student)
        {
           
            

            if (ModelState.IsValid)
            {
                StudentRepository.Update(id, Student);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(id);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            StudentRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
