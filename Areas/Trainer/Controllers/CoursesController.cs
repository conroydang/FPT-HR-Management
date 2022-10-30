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
    public class CoursesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Trainer/Courses
        public ActionResult Index()
        {
            var user = (User)Session["User"];
            var trainee = db.Trainees.FirstOrDefault(x => x.IDUser == user.Id);


            var trainingStaffs = db.TrainingStaffs.Where(x => x.IdTrainee == trainee.Id).ToList();
                
            List<Course> courseList = new List<Course>();
            foreach (var staff in trainingStaffs)
            {
                courseList.Add(staff.Course);
            }
            return View(courseList);
  
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
