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
            /*ICollection<Activity> activities;
            using (var service = new ActivitiesService()){
                activities = service.GetAllActivities();
                //activities = service.GetAllTopActivities();
            }
            foreach(Activity a in activities)
            {
                a.Picture = null;
            }
            return View(activities);*/
            return View(GetActivities(1));
        }

        [HttpPost]
        public ActionResult Index(int currentPage)
        {
            return View(GetActivities(currentPage));
        }


        [HttpPost]
        public ActionResult Create(string category, string name, string des, HttpPostedFileBase file)
        {
            using (var service = new ActivitiesService())
            {
                var activity = new Activity();
                activity.Category = category;
                activity.PictureName = name;
                activity.Description = des;
                
                System.Drawing.Image sourceimage = System.Drawing.Image.FromStream(file.InputStream);
                activity.Picture = SUCSA.DATA.ByteHelper.ImageToByteArray(sourceimage);

                activity.IsTop = false;
                service.AddActivity(activity);
            }
            return RedirectToAction("Index");
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

        [HttpPost]
        public JsonResult GetPicture(int id)
        {
            byte[] pic;
            using (var service = new ActivitiesService())
            {
                pic = service.GetActivityById(id).Picture;
            }
            var base64 = Convert.ToBase64String(pic);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            return Json(imgSrc);
        }

        private SUCSA.Models.ActivityViewModels GetActivities(int currentPage)
        {
            //int maxRows = 10;
            int maxRows = 3;
            using (var service = new ActivitiesService())
            {
                SUCSA.Models.ActivityViewModels activityModels = new SUCSA.Models.ActivityViewModels();

                activityModels.activities = service.GetActivitiesInARange(currentPage, maxRows);
                    

                double pageCount = (double)((decimal)service.CountActivities() / Convert.ToDecimal(maxRows));
                activityModels.PageCount = (int)Math.Ceiling(pageCount);

                activityModels.CurrentPageIndex = currentPage;

                return activityModels;
            }
        }
    }
}