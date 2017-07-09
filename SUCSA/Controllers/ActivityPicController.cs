using SUCSA.DATA;
using SUCSA.Models;
using SUCSA.SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUCSA.Controllers
{
    public class ActivityPicController : Controller
    {
        // GET: ActivityPic
        public ActionResult Index()
        {
            IList<Activity> activities = new List<Activity>();
            using (var service=new ActivitiesService())
            {
                activities = service.GetAllActivities().ToList();
            }
            ActivityPicViewModels models = new ActivityPicViewModels(activities);
            return View(models);
        }
    }
}