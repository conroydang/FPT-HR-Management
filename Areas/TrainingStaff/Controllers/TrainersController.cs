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
    public class TrainersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/Trainers
        public ActionResult Index(string Search)
        {

            var trainers = db.Trainers.Include(t => t.User);
            if (string.IsNullOrEmpty(Search))
            {
                return View(trainers.Where(x => x.IsActive == true));
            }
            return View(trainers.Where(x => x.User.Name.ToLower().Contains(Search.ToLower())));

        }

        // GET: Admin/Trainers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Admin/Trainers/Create
        public ActionResult Create()
        {
            ViewBag.IDUser = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Admin/Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IDUser,IsActive")] StaffManager.Models.Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                trainer.IsActive = true;
                db.Trainers.Add(trainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDUser = new SelectList(db.Users, "Id", "Name", trainer.IDUser);
            return View(trainer);
        }

        // GET: Admin/Trainers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDUser = new SelectList(db.Users, "Id", "Name", trainer.IDUser);
            return View(trainer);
        }

        // POST: Admin/Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IDUser,IsActive")] StaffManager.Models.Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                trainer.IsActive = true;
                db.Entry(trainer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDUser = new SelectList(db.Users, "Id", "Name", trainer.IDUser);
            return View(trainer);
        }

        // GET: Admin/Trainers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Admin/Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var trainer = db.Trainers.Find(id);
            trainer.IsActive = false;
            db.Entry(trainer).State = System.Data.Entity.EntityState.Modified;
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
