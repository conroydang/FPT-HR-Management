using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StaffManager.Models;

namespace StaffManager.Areas.Trainer.Controllers
{
    public class UsersController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Edit()
        {
            User user = (User)Session["User"];

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Role,Name,Email,Password,Dob,Address,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Users.FirstOrDefault(x=>x.Email==user.Email);
                int role = obj.Role;
                user.Role = role;
                user.IsActive = true;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            ViewBag.Role = new SelectList(db.Roles, "Id", "Title", user.Role);
            return View(user);
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
