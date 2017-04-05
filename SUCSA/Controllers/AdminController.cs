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
                //activities = service.GetAllTopActivities();
            }
            return View(activities);
        }

        public ActionResult reverseTop(int id)
        {
            using(var service = new ActivitiesService()){
                var activity = service.GetActivityById(id);
                activity.IsTop = !activity.IsTop;
                service.updateActivity(activity);
            }
            return RedirectToAction("Index");
        }

        public ActionResult update(int id, string name, string des)
        {
            using(var service =  new ActivitiesService()){
                var activity = service.GetActivityById(id);
                activity.PictureName = name;
                activity.Description = des;
                service.updateActivity(activity);
            }
            return RedirectToAction("Index");
        }

        public ActionResult delete(int id)
        {
            using (var service = new ActivitiesService())
            {
                var activity = service.GetActivityById(id);
                service.RemoveActivity(activity);
            }
            return RedirectToAction("Index");
        }
    }
}