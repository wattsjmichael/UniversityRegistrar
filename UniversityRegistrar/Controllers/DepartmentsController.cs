using System;
using System.Collections.Generic;
using UniversityRegistrar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UniversityRegistrar.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly UniversityRegistrarContext _db;
        public DepartmentsController(UniversityRegistrarContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            List<Department> model = _db.Departments.OrderBy(x => x.DepartmentName).ToList(); 
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department department, int DepartmentId)
        {
            _db.Departments.Add(department);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            Department thisDepartment = _db.Departments
            .Include(department => department.Students)
            .Include(department => department.Courses)
            .FirstOrDefault(department => department.DepartmentId == id);
            return View(thisDepartment);
        }
        public ActionResult Edit(int id)
        {                    
            Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
            return View(thisDepartment);
        }
        [HttpPost]
        public ActionResult Edit(Department department, int id)
        {
            _db.Entry(department).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
            return View(thisDepartment);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
            _db.Departments.Remove(thisDepartment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}