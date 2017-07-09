using SUCSA.DATA;
using SUCSA.Models;
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
            IList<Supplier> suppliers = new List<Supplier>();
            using (var service=new ActivitiesService())
            {
                activities = service.GetAllTopActivities().ToList();
            }
            using (var service = new SupplierService())
            {
                suppliers = service.GetAllSuppliers().ToList();
            }

            HomePageViewModels models = new HomePageViewModels(activities, suppliers);
            return View(models);
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