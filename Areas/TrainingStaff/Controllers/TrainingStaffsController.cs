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
    public class TrainingStaffsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: TrainingStaff/TrainingStaffs
        public ActionResult Index(string Search)
        {
            var trainingStaffs = db.TrainingStaffs.Include(t => t.Course).Include(t => t.Trainee).Include(t => t.Trainer);
            if (string.IsNullOrEmpty(Search))
            {
                return View(trainingStaffs.Where(x => x.IsActive == true).ToList());
            }
            return View(trainingStaffs.Where(x => x.Name.Contains(Search)).ToList());


        }

        // GET: TrainingStaff/TrainingStaffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TrainingStaff trainingStaff = db.TrainingStaffs.Find(id);
            if (trainingStaff == null)
            {
                return HttpNotFound();
            }
            return View(trainingStaff);
        }

        // GET: TrainingStaff/TrainingStaffs/Create
        public ActionResult Create()
        {
            ViewBag.IdCourse = new SelectList(db.Courses, "Id", "Name");
            ViewBag.IdTrainee = new SelectList(db.Trainees, "Id", "Name");
            ViewBag.IdTrainer = new SelectList(db.Trainers, "Id", "Name");
            return View();
        }

        // POST: TrainingStaff/TrainingStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IdTrainee,IdTrainer,IdCourse,IsActive")] Models.TrainingStaff trainingStaff)
        {
            if (ModelState.IsValid)
            {
                trainingStaff.IsActive = true;
                db.TrainingStaffs.Add(trainingStaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCourse = new SelectList(db.Courses, "Id", "Name", trainingStaff.IdCourse);
            ViewBag.IdTrainee = new SelectList(db.Trainees, "Id", "Name", trainingStaff.IdTrainee);
            ViewBag.IdTrainer = new SelectList(db.Trainers, "Id", "Name", trainingStaff.IdTrainer);
            return View(trainingStaff);
        }

        // GET: TrainingStaff/TrainingStaffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TrainingStaff trainingStaff = db.TrainingStaffs.Find(id);
            if (trainingStaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCourse = new SelectList(db.Courses, "Id", "Name", trainingStaff.IdCourse);
            ViewBag.IdTrainee = new SelectList(db.Trainees, "Id", "Name", trainingStaff.IdTrainee);
            ViewBag.IdTrainer = new SelectList(db.Trainers, "Id", "Name", trainingStaff.IdTrainer);
            return View(trainingStaff);
        }

        // POST: TrainingStaff/TrainingStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IdTrainee,IdTrainer,IdCourse,IsActive")] Models.TrainingStaff trainingStaff)
        {
            if (ModelState.IsValid)
            {
                trainingStaff.IsActive = true;
                db.Entry(trainingStaff).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCourse = new SelectList(db.Courses, "Id", "Name", trainingStaff.IdCourse);
            ViewBag.IdTrainee = new SelectList(db.Trainees, "Id", "Name", trainingStaff.IdTrainee);
            ViewBag.IdTrainer = new SelectList(db.Trainers, "Id", "Name", trainingStaff.IdTrainer);
            return View(trainingStaff);
        }

        // GET: TrainingStaff/TrainingStaffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.TrainingStaff trainingStaff = db.TrainingStaffs.Find(id);
            if (trainingStaff == null)
            {
                return HttpNotFound();
            }
            return View(trainingStaff);
        }

        // POST: TrainingStaff/TrainingStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.TrainingStaff trainingStaff = db.TrainingStaffs.Find(id);
            trainingStaff.IsActive = false;
            db.Entry(trainingStaff).State = System.Data.Entity.EntityState.Modified;
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
