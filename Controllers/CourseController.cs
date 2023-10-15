using Future_Generation.Common;
using Future_Generation.Models;
using Future_Generation.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Future_Generation.Controllers
{
    public class CourseController : Controller
    {
       
        public ICourseRepository CourseRepository { get; }
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IFormFile syllabusAttachmentFile;
        public CourseController(ICourseRepository _CourseRepository, IWebHostEnvironment _webHostEnvironment)
        {
            CourseRepository = _CourseRepository;
            webHostEnvironment = _webHostEnvironment;
        }

        public ActionResult Index()
        {
            return View("Index", CourseRepository.GetAll());
        }
        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            string CourseName = "";


            var Students = CourseRepository.GetAll();
            if (!string.IsNullOrEmpty(collection["CourseName"]))
            {
                CourseName = collection["CourseName"];
            }


            if (!string.IsNullOrEmpty(CourseName))
            {
                return View(Students.Where(e => e.CourseName.Contains(CourseName)));
            }
           
            else
            {
                return RedirectToAction("Index");
            }
        }

        //public IActionResult DownloadSyllabus(int courseId)
        //{
            
        //    var course = CourseRepository.GetById(courseId);

        //    if (course != null && course.SyllabusAttachment != null)
        //    {
              
        //        return File(course.SyllabusAttachment, "application/octet-stream", "Syllabus.pdf");
        //    }

            
        //    return View("Error");
        //}

        public ActionResult Details(int id)

        {
            return View("Details", CourseRepository.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Course Course, IFormFile syllabusAttachmentFile)
        {
            if (syllabusAttachmentFile.Length > 10)
            {
                ModelState.AddModelError("syllabusAttachmentFile", "The file size should be 10 bytes or less.");
            }
            Course.SyllabusAttachment = syllabusAttachmentFile.FileName.ToString();
            //if (ModelState.IsValid)
            //{
                try
                {
                    CourseRepository.Add(Course, syllabusAttachmentFile, webHostEnvironment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the course.");
                    Console.WriteLine(ex.ToString());
                }
            //}

            return View();
        }



        public ActionResult Edit(int id)
        {
            return View(CourseRepository.GetById(id));
        }
        [HttpPost]

        public ActionResult Edit(int id, Course Course, IFormFile syllabusAttachmentFile, IFormFile newSyllabusAttachmentFile)
        {
            IFormFile updatedSyllabusAttachmentFile = null;

            if (newSyllabusAttachmentFile != null && newSyllabusAttachmentFile.Length > 0)
            {
                updatedSyllabusAttachmentFile = newSyllabusAttachmentFile;
                Course.SyllabusAttachment = updatedSyllabusAttachmentFile.FileName.ToString();
            }

            CourseRepository.Update(id, Course, updatedSyllabusAttachmentFile, webHostEnvironment);
            return RedirectToAction(nameof(Index));
        }





        public ActionResult Delete(int id)
        {
            return View(id);
        }

        [HttpPost, ActionName("Delete")]
   
        public ActionResult DeleteConfirmed(int id)
        {
            CourseRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
