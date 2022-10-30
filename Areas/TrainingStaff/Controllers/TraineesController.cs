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
    public class TraineesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/Trainees
        public ActionResult Index(string Search)
        {
            var trainees = db.Trainees.Include(t => t.User);
            if (string.IsNullOrEmpty(Search))
            {
                return View(trainees.Where(x => x.IsActive == true));
            }
            return View(trainees.Where(x => x.User.Name.ToLower().Contains(Search.ToLower())));
        }

        // GET: Admin/Trainees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // GET: Admin/Trainees/Create
        public ActionResult Create()
        {
            ViewBag.IDUser = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Admin/Trainees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IDUser,Note,IsActive")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                trainee.IsActive = true;
                db.Trainees.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDUser = new SelectList(db.Users, "Id", "Name", trainee.IDUser);
            return View(trainee);
        }

        // GET: Admin/Trainees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDUser = new SelectList(db.Users, "Id", "Name", trainee.IDUser);
            return View(trainee);
        }

        // POST: Admin/Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IDUser,Note,IsActive")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                trainee.IsActive = true;
                db.Entry(trainee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDUser = new SelectList(db.Users, "Id", "Name", trainee.IDUser);
            return View(trainee);
        }

        // GET: Admin/Trainees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Admin/Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            trainee.IsActive = false;
            db.Entry(trainee).State = System.Data.Entity.EntityState.Modified;
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
