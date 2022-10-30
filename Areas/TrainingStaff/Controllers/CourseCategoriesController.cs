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
    public class CourseCategoriesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: TrainingStaff/CourseCategories
        public ActionResult Index(string Search)
        {
            if (string.IsNullOrEmpty(Search))
            {
                return View(db.CourseCategories.Where(x=>x.IsActive==true).ToList());
            }
            return View(db.CourseCategories.Where(x=>x.Name.Contains(Search)||x.Content.Contains(Search)).ToList());
        }

        // GET: TrainingStaff/CourseCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }
            return View(courseCategory);
        }

        // GET: TrainingStaff/CourseCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingStaff/CourseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Content,IsActive")] CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                courseCategory.IsActive = true;
                db.CourseCategories.Add(courseCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseCategory);
        }

        // GET: TrainingStaff/CourseCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }
            return View(courseCategory);
        }

        // POST: TrainingStaff/CourseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Content,IsActive")] CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                courseCategory.IsActive = true;
                db.Entry(courseCategory).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseCategory);
        }

        // GET: TrainingStaff/CourseCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }
            return View(courseCategory);
        }

        // POST: TrainingStaff/CourseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            courseCategory.IsActive = false;
            db.Entry(courseCategory).State = System.Data.Entity.EntityState.Modified;
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
