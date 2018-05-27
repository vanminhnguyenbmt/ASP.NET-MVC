using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Common;
using BanDongHo.Areas.Admin.Models;

namespace BanDongHo.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel )
        {
            ViewBag.ErrorMessage = "";
            if (ModelState.IsValid)
            {
                LoginService loginService = new LoginService();
                var result = loginService.Login(loginViewModel.UserName, Encryptor.MD5Hash(loginViewModel.Password));
                if(result == 1)
                {
                    var user = loginService.GetUserByName(loginViewModel.UserName);
                    var userSession = new UserLogin();
                    userSession.UserID = user.MATK;
                    userSession.UserName = user.TENDN;                   
                    Session.Add(CommonConstands.ADMIN_SESSION, userSession);
                    return Redirect("~/Admin/Home/Index");
                }else if(result == 0)
                {
                    ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng";
                }else if(result == -1)
                {
                    ViewBag.ErrorMessage = "Tài khoản này không đủ quyền truy cập trang Admin";
                }else
                {
                    ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng";
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng";
               
            }
            return View(loginViewModel);
        }
    }
}