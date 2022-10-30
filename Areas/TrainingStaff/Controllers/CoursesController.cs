using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StaffManager.Models;

namespace StaffManager.Areas.TrainingStaff.Controllers
{
    public class CoursesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: TrainingStaff/Courses
        public ActionResult Index(string Search)
        {
            var courses = db.Courses.Include(c => c.CourseCategory);
            if (!string.IsNullOrEmpty(Search))
            {
                return View(courses.Where(x => x.Name.Contains(Search)).ToList());
            }

            return View(courses.Where(x => x.IsActive == true).ToList());
        }

        // GET: TrainingStaff/Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: TrainingStaff/Courses/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.CourseCategories, "Id", "Name");
            return View();
        }

        // POST: TrainingStaff/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Picture,LastUpdate,Category,IsActive")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.IsActive = true;
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.CourseCategories, "Id", "Name", course.Category);
            return View(course);
        }

        // GET: TrainingStaff/Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.CourseCategories, "Id", "Name", course.Category);
            return View(course);
        }

        // POST: TrainingStaff/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Picture,LastUpdate,Category,IsActive")] Course course)
        {
            if (ModelState.IsValid)
            {course.IsActive = true;
                db.Entry(course).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.CourseCategories, "Id", "Name", course.Category);
            return View(course);
        }

        // GET: TrainingStaff/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: TrainingStaff/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            course.IsActive = false;
            db.Entry(course).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
