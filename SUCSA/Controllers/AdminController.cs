﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUCSA.SERVICE;
using SUCSA.DATA;
using SUCSA.Models;

namespace SUCSA.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Supplier()
        {
            return View(GetSupplier(1));
        }

        [HttpPost]
        public ActionResult Supplier(int currentPage)
        {
            return View(GetSupplier(currentPage));
        }

        [HttpPost]
        public ActionResult CreateSupplier(string name, string des, HttpPostedFileBase file)
        {
            using (var service = new SupplierService())
            {
                var supplier = new Supplier();
                supplier.SupplierName = name;
                supplier.Description = des;

                System.Drawing.Image sourceimage = System.Drawing.Image.FromStream(file.InputStream);
                supplier.Picture = SUCSA.DATA.ByteHelper.ImageToByteArray(sourceimage);

                supplier.IsTop = false;
                service.AddSupplier(supplier);
            }
            return RedirectToAction("Supplier");
        }

        public ActionResult UpdateSupplier(int id, string name, string des)
        {
            using (var service = new SupplierService())
            {
                var supplier = service.GetSupplierByID(id);
                supplier.SupplierName = name;
                supplier.Description = des;
                service.updateSupplier(supplier);
            }
            return RedirectToAction("Supplier");
        }

        public ActionResult DeleteSupplier(int id)
        {
            using (var service = new SupplierService())
            {
                var supplier = service.GetSupplierByID(id);
                service.RemoveSupplier(supplier);
            }
            return RedirectToAction("Supplier");
        }

        [HttpPost]
        public JsonResult GetPictureSupplier(int id)
        {
            byte[] pic;
            using (var service = new SupplierService())
            {
                pic = service.GetSupplierByID(id).Picture;
            }
            var base64 = Convert.ToBase64String(pic);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            return Json(imgSrc);
        }

        public ActionResult ReverseTopSupplier(int id)
        {
            using (var service = new SupplierService())
            {
                var supplier = service.GetSupplierByID(id);
                supplier.IsTop = !supplier.IsTop;
                service.updateSupplier(supplier);
            }
            return RedirectToAction("Supplier");
        }

        private SUCSA.Models.SupplierViewModels GetSupplier(int currentPage)
        {
            //int maxRows = 10;
            int maxRows = 3;
            using (var service = new SupplierService())
            {
                SUCSA.Models.SupplierViewModels supplierModels = new SUCSA.Models.SupplierViewModels();
                supplierModels.activities = service.GetSupplierInARange(currentPage, maxRows);
                double pageCount = (double)((decimal)service.CountSuppliers() / Convert.ToDecimal(maxRows));
                supplierModels.PageCount = (int)Math.Ceiling(pageCount);
                supplierModels.CurrentPageIndex = currentPage;
                return supplierModels;
            }
        }
    
        public ActionResult Activity()
        {
            return View(GetActivities(1));
        }

        [HttpPost]
        public ActionResult Activity(int currentPage)
        {
            return View(GetActivities(currentPage));
        }


        [HttpPost]
        public ActionResult CreateActivity(int? category, string new_activity, string name, string des, HttpPostedFileBase file, HttpPostedFileBase newThumbnail)
        {
            using (var service = new ActivitiesService())
            {
                var activity = new Activity();
                if (new_activity!=null)
                {
                    AcitivityCategory newCategory = new AcitivityCategory();
                    newCategory.CategoryName = new_activity;
                    service.AddCategory(newCategory);
                    category = service.GetCategoryByName(new_activity).CategoryId;

                }
                activity.CategoryId = (int)category;
                activity.PictureName = name;
                activity.Description = des;

                System.Drawing.Image sourceimage = System.Drawing.Image.FromStream(file.InputStream);
                activity.Picture = SUCSA.DATA.ByteHelper.ImageToByteArray(sourceimage);

                System.Drawing.Image thumbnail = System.Drawing.Image.FromStream(newThumbnail.InputStream);
                activity.Thumbnails = SUCSA.DATA.ByteHelper.ImageToByteArray(thumbnail);

                activity.IsTop = false;
                service.AddActivity(activity);
            }
            return RedirectToAction("Activity");
        }

        public ActionResult ReverseTopActivity(int id)
        {
            using(var service = new ActivitiesService()){
                var activity = service.GetActivityById(id);
                activity.IsTop = !activity.IsTop;
                service.updateActivity(activity);
            }
            return RedirectToAction("Activity");
        }

        public ActionResult UpdateActivity(int id,int cat,string name, string des)
        {
            using(var service =  new ActivitiesService()){
                var activity = service.GetActivityById(id);
                activity.CategoryId = cat;
                activity.PictureName = name;
                activity.Description = des;
                service.updateActivity(activity);
            }
            return RedirectToAction("Activity");
        }

        public ActionResult DeleteActivity(int id)
        {
            using (var service = new ActivitiesService())
            {
                var activity = service.GetActivityById(id);
                service.RemoveActivity(activity);
            }
            return RedirectToAction("Activity");
        }

        [HttpPost]
        public JsonResult GetPictureActivity(int id)
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

        private ActivityViewModels GetActivities(int currentPage)
        {
           int maxRows = 5;
            ActivityViewModels activityModels = new ActivityViewModels();
            var service = new ActivitiesService();        
            activityModels.activities = service.GetActivitiesInARange(currentPage, maxRows);
            double pageCount = (double)((decimal)service.CountActivities() / Convert.ToDecimal(maxRows));
            activityModels.PageCount = (int)Math.Ceiling(pageCount);
            activityModels.CurrentPageIndex = currentPage;
            return activityModels;
        }

        public ActionResult Admin()
        {
            return View(GetAdmins(1));
        }

        [HttpPost]
        public ActionResult Admin(int currentPage)
        {
            return View(GetAdmins(currentPage));
        }

        private AccountViewModels GetAdmins(int currentPage)
        {
            int maxRows = 5;
            AccountViewModels accountModels = new AccountViewModels();
            var service = new AdminService();
            accountModels.Admins = service.GetAdminsInARange(currentPage, maxRows);
            double pageCount = (double)((decimal)service.CountAdmins() / Convert.ToDecimal(maxRows));
            accountModels.PageCount = (int)Math.Ceiling(pageCount);
            accountModels.CurrentPageIndex = currentPage;
            return accountModels;
        }

        public ActionResult DeleteAdmin(int id)
        {
            using (var service = new AdminService())
            {
                service.DeleteAdmin(id);
            }
            return RedirectToAction("Admin");
        }

        public ActionResult CreateAdmin(string newAdmin, string newPassowrd)
        {
            using (var service = new AdminService())
            {
                if (service.GetAdminByName(newAdmin) != null)
                {
                    TempData["notice"] = "创建失败，用户名已存在！";
                    return RedirectToAction("Admin");
                }
                var admin = new Admin();
                admin.UserName = newAdmin;
                admin.PassWord = newPassowrd;
                service.AddAdmin(admin);
            }
            return RedirectToAction("Admin");
        }
    }
}