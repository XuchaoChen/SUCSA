using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SUCSA.Models;
using SUCSA.SERVICE;
using System.Web.Security;
using SUCSA.DATA;

namespace SUCSA.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ModelState.Clear();
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (!Login(model))
                {
                    ModelState.AddModelError("", "无效的用户名或密码。");
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Username,model.RememberMe);
                    return RedirectToAction("Activity", "Admin");
                }
            }
        }

        private bool Login(LoginViewModel model)
        {
            using(var service=new AdminService())
            {
                var user = service.GetAdminByName(model.Username);
                if (user == null||user.PassWord!=service.Encrypt(model.Password))
                {
                    return false;
                }
            }
            return true;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            ModelState.Clear();
            return View("ResetPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View("RestPassword");
            }
            using (var service=new AdminService())
            {
                var admin = service.GetAdminByName(userName);
                if (admin.PassWord != model.OldPassword)
                {
                    ModelState.AddModelError("", "旧密码不正确");
                    return View();
                }
                service.ChangePassword(admin.AdminID, model.NewPassword);
            }
            return RedirectToAction("Activity", "Admin");
        }
    }
       
}