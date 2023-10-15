using Future_Generation.Common;
using Future_Generation.Models;
using Future_Generation.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Buffers.Text;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Future_Generation.RepoServices
{
    public class CourseRepositoryService : ICourseRepository
    {
        public ApplicationDbContext Context { get; }
        public CourseRepositoryService(ApplicationDbContext _Context)
        {
            Context= _Context;
        }
        public List<Course> GetAll()
        {
            var Courses = Context.Courses
               .Include(c => c.StudentCourses)
               .ThenInclude(c => c.Student).ToList();
            return Courses;
        }
        public void Add(Course Course, IFormFile syllabusAttachmentFile, IWebHostEnvironment webHostEnvironment)
        {
            try
            {
                if (syllabusAttachmentFile != null && syllabusAttachmentFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Attachment");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + syllabusAttachmentFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        syllabusAttachmentFile.CopyTo(fileStream);
                    }

                    Course.SyllabusAttachment = filePath; 
                }

                
                Context.Courses.Add(Course);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw; 
            }
        }



        public void Delete(int id)
        {
            var deletedCourse = Context.Courses.Where(d => d.CourseId == id).FirstOrDefault();
            if (deletedCourse != null)
            {
                Context.Courses.Remove(deletedCourse);
                Context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Department not found");
            }
        }

      
        public Course GetById(int id)
    {
        var Course = Context.Courses.Where(c => c.CourseId == id)
        .Include(c => c.StudentCourses)
        .ThenInclude(c => c.Student).FirstOrDefault();
        if (Course == null)
        {
            Console.WriteLine("Course not found");
        }
        return Course;
    }

        public void Update(int id, Course Course, IFormFile updatedSyllabusAttachmentFile, IWebHostEnvironment webHostEnvironment)
        {
            var existingCourse = Context.Courses.FirstOrDefault(d => d.CourseId == id);

            if (existingCourse != null)
            {
                existingCourse.CourseName = Course.CourseName;
                existingCourse.StartDate = Course.StartDate;
                existingCourse.EndDate = Course.EndDate;
                existingCourse.Cost = Course.Cost;
                existingCourse.Capacity = Course.Capacity;
                existingCourse.Status = Course.Status;

                if (updatedSyllabusAttachmentFile != null && updatedSyllabusAttachmentFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Attachment");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + updatedSyllabusAttachmentFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        updatedSyllabusAttachmentFile.CopyTo(fileStream);
                    }

                    existingCourse.SyllabusAttachment = filePath;
                }

                Context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Course not found");
            }
        }

        public void Add(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
