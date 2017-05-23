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

namespace SUCSA.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
           // if(FormsAuthentication.GetAuthCookie())
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
                    FormsAuthentication.SetAuthCookie(model.Username,false);
                    return RedirectToAction("Activity", "Admin");
                }
            }

        }

        private bool Login(LoginViewModel model)
        {
            using(var service=new AdminService())
            {
                var user = service.GetAdminByName(model.Username);
                if (user == null||user.PassWord!=model.Password)
                {
                    return false;
                }
            }
            return true;
        }
    }
       
}