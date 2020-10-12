using System;
using System.Collections.Generic;
using UniversityRegistrar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UniversityRegistrar.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UniversityRegistrarContext _db;
        public CoursesController(UniversityRegistrarContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            List<Course> model = _db.Courses.OrderBy(x => x.CourseName).ToList(); 
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Course course, int StudentId)
        {
            _db.Courses.Add(course);
            // if (StudentId != 0)
            // {
            //     _db.CourseStudent.Add(new CourseStudent() { CourseId = course.CourseId, StudentId = StudentId });
            // }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            Course thisCourse = _db.Courses
            .Include(course => course.Students)
            .ThenInclude(join => join.Student)
            .FirstOrDefault(course => course.CourseId == id);
            return View(thisCourse);
        }
        public ActionResult Edit(int id)
        {
            Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
            ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
            return View(thisCourse);
        }
        [HttpPost]
        public ActionResult Edit(Course course, int id)
        {
            // if (CourseId != 0)
            // {
            //     _db.CourseStudent.Add(new CourseStudent() { CourseId = CourseId, StudentId = student.StudentId});
            // }
            _db.Entry(course).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
            return View(thisCourse);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
            _db.Courses.Remove(thisCourse);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddStudent(int id)
        {
            Course thisCourse = _db.Courses.FirstOrDefault(s => s.CourseId == id);
            ViewBag.Studentid = new SelectList(_db.Students, "StudentId", "Name");
            return View(thisCourse);
        }
        [HttpPost]
        public ActionResult AddStudent(Course course, int StudentId)
        {
            if (StudentId != 0)
            {
                if(_db.CourseStudent.Where(x => x.StudentId == StudentId && x.CourseId == course.CourseId).ToHashSet().Count == 0)
                {
                    _db.CourseStudent.Add(new CourseStudent() { CourseId = course.CourseId, StudentId = StudentId });

                }
                
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteStudent(int CourseStudentId)
        {
            CourseStudent joinEntry = _db.CourseStudent.FirstOrDefault(entry => entry.CourseStudentId == CourseStudentId);
            _db.CourseStudent.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}