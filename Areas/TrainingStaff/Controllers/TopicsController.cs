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
    public class TopicsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: TrainingStaff/Topics
        public ActionResult Index(string Search)
        {
            var topics = db.Topics.Include(t => t.Course1);
            if (string.IsNullOrEmpty(Search))
            {
                return View(topics.Where(x=>x.IsActive== true).ToList());
            }
            return View(topics.Where(x=>x.Name.Contains(Search)).ToList());
           
            
        }

        // GET: TrainingStaff/Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: TrainingStaff/Topics/Create
        public ActionResult Create()
        {
            ViewBag.Course = new SelectList(db.Courses, "Id", "Name");
            return View();
        }

        // POST: TrainingStaff/Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Content,Field,Course,IsActive")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.IsActive = true;
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course = new SelectList(db.Courses, "Id", "Name", topic.Course);
            return View(topic);
        }

        // GET: TrainingStaff/Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course = new SelectList(db.Courses, "Id", "Name", topic.Course);
            return View(topic);
        }

        // POST: TrainingStaff/Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Content,Field,Course,IsActive")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.IsActive = true;
                db.Entry(topic).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course = new SelectList(db.Courses, "Id", "Name", topic.Course);
            return View(topic);
        }

        // GET: TrainingStaff/Topics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: TrainingStaff/Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            topic.IsActive = false;
            db.Entry(topic).State = System.Data.Entity.EntityState.Modified;
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
