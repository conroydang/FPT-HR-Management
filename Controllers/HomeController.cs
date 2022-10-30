
using StaffManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StaffManager.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.Login(model.Email, model.Password).ToList();
                Console.WriteLine(result.Count());
                if (result.Any())
                {
                    var user = result.FirstOrDefault();
                    User acc = new User();
                    acc.Name = user.Name;
                    acc.Email = user.Email;
                    acc.Password = user.Password;
                    acc.Dob = user.Dob;
                    acc.Address = user.Address;
                    acc.Id = user.Id;
                   
                    Session["User"] = acc;
                    if (user.Title.ToLower().Contains("administrator"))
                    {
                        return RedirectToAction("Index", "Trainers", new { area = "Admin" });
                    }
                    else if (user.Title.ToLower().Contains("training staff"))
                    {
                        return RedirectToAction("Index", "TrainingStaffs", new { area = "TrainingStaff" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Courses", new { area = "Trainer" });
                    }
                }
                else
                {
                    ViewBag.ERROR = "Tài khoản hoặc mật khẩu không chính xác";
                    return View();
                }
            }
            return View();

        }

    }
}