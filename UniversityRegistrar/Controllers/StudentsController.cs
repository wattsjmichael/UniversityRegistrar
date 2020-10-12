using System;
using System.Collections.Generic;
using UniversityRegistrar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UniversityRegistrar.Controllers
{
    
    public class StudentsController : Controller
    {
        private readonly UniversityRegistrarContext _db;
        public StudentsController(UniversityRegistrarContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            List<Student> model = _db.Students.OrderBy(x => x.Name).ToList(); 
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student, int CourseId)
        {
            _db.Students.Add(student);
            if (CourseId != 0)
            {
                _db.CourseStudent.Add(new CourseStudent() { CourseId = CourseId, StudentId = student.StudentId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            Student thisStudent = _db.Students
            .Include(student => student.Courses)
            .ThenInclude(join => join.Course)
            .FirstOrDefault(student => student.StudentId == id);
            return View(thisStudent);
        }
        public ActionResult Edit(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
            return View(thisStudent);
        }
        [HttpPost]
        public ActionResult Edit(Student student, int id)
        {
            // if (CourseId != 0)
            // {
            //     _db.CourseStudent.Add(new CourseStudent() { CourseId = CourseId, StudentId = student.StudentId});
            // }
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var thisItem = _db.Students.FirstOrDefault(x => x.StudentId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisItem = _db.Students.FirstOrDefault(x => x.StudentId == id);
            _db.Students.Remove(thisItem); 
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddCourse(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(s => s.StudentId == id);
            ViewBag.Courseid = new SelectList(_db.Courses, "CourseId", "CourseName");
            return View(thisStudent);
        }
        [HttpPost]
        public ActionResult AddCourse(Student student, int CourseId)
        {
            if (CourseId != 0)
            {
                if(_db.CourseStudent.Where(x => x.StudentId == student.StudentId && x.CourseId == CourseId).ToHashSet().Count == 0)
                {
                    _db.CourseStudent.Add(new CourseStudent() { CourseId = CourseId, StudentId = student.StudentId });

                }
                
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}