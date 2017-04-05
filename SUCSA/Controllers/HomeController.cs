using SUCSA.DATA;
using SUCSA.SERVICE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUCSA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IList<Activity> activities = new List<Activity>();
            using(var service=new ActivitiesService())
            {
                activities = (IList<Activity>)service.GetAllTopActivities();
            }
            return View(activities);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}