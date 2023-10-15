using Future_Generation.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Future_Generation.Controllers
{
    public class StudentCoursesController : Controller
    {
        public ICourseRepository CourseRepository { get; }
        public IRepository<Student> StudentRepository { get; }
        public IRepository<StudentCourses> StudentCoursesRepository { get; }

    public StudentCoursesController(ICourseRepository _CourseRepository, IRepository<Student> _StudentRepository, IRepository<StudentCourses> _StudentCoursesRepository)
        {
            CourseRepository = _CourseRepository;
            StudentRepository = _StudentRepository;
            StudentCoursesRepository = _StudentCoursesRepository;
        }
        public ActionResult Index()
        {
            ViewBag.stdList = new SelectList(StudentRepository.GetAll(), "Id", "Name");
            ViewBag.crsList = new SelectList(CourseRepository.GetAll(), "CourseId", "CourseName");
            return View("Index", StudentCoursesRepository.GetAll());
        }
        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            int stdId = 0;
            int crsId = 0;

            if (!string.IsNullOrEmpty(collection["Student"]))
            {
                stdId = int.Parse(collection["Student"]);
            }
            if (!string.IsNullOrEmpty(collection["Course"]))
            {
                crsId = int.Parse(collection["Course"]);
            }

            ViewBag.stdList = new SelectList(StudentRepository.GetAll(), "Id", "Name");
            ViewBag.crsList = new SelectList(CourseRepository.GetAll(), "CourseId", "CourseName");

            if (stdId != 0 && crsId != 0)
            {
                return View(StudentCoursesRepository.GetAll()
                        .Where(c => c.StudentId == stdId && c.CourseId == crsId).ToList());
            }
            else if (stdId != 0)
            {
                return View(StudentCoursesRepository.GetAll()
                        .Where(c => c.CourseId == stdId).ToList());
            }
            else if (crsId != 0)
            {
                return View(StudentCoursesRepository.GetAll()
                        .Where(c => c.StudentId == crsId).ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            return View(StudentCoursesRepository.GetById(id));
        }

        public ActionResult Create()
        {
            ViewBag.stdList = new SelectList(StudentRepository.GetAll(), "Id", "Name");
            ViewBag.crsList = new SelectList(CourseRepository.GetAll(), "CourseId", "CourseName");

            return View(new StudentCourses());
        }

        [HttpPost]
        public ActionResult Create(StudentCourses studentCourses)
        {
            ViewBag.stdList = new SelectList(StudentRepository.GetAll(), "Id", "Name");
            ViewBag.crsList = new SelectList(CourseRepository.GetAll(), "CourseId", "CourseName");

            
            var Uniq = StudentCoursesRepository.GetAll()
                .Any(c => c.StudentId == studentCourses.StudentId && c.CourseId == studentCourses.CourseId);

            if (Uniq)
            {
                ModelState.AddModelError("All", "This Record Exists");
            }

            if (!Uniq) 
            {
                StudentCoursesRepository.Add(studentCourses);
                return RedirectToAction(nameof(Index));
            }

            return View(studentCourses); 
        }




        public ActionResult Edit(int id)
        {
            ViewBag.stdList = new SelectList(StudentRepository.GetAll(), "Id", "Name");
            ViewBag.crsList = new SelectList(CourseRepository.GetAll(), "CourseId", "CourseName");

            return View(StudentCoursesRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, StudentCourses StudentCourses)
        {
            ViewBag.stdList = new SelectList(StudentRepository.GetAll(), "Id", "Name");
            ViewBag.crsList = new SelectList(CourseRepository.GetAll(), "CourseId", "CourseName");

            var Uniq = StudentCoursesRepository.GetAll()
                .Any(c => c.StudentId == StudentCourses.StudentId && c.CourseId == StudentCourses.CourseId);
            if (Uniq)
            {
                ModelState.AddModelError("All", "This Record Exist");
            }

            if (!Uniq)
            {
                StudentCoursesRepository.Update(id, StudentCourses);
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
            StudentCoursesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
