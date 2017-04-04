using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUCSA.SERVICE;
using SUCSA.DATA;

namespace SUCSA.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ICollection<Activity> activities;
            using (var service = new ActivitiesService()){
                activities = service.GetAllActivities();
            }
            return View(activities);
        }
    }
}