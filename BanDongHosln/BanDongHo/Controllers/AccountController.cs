using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models.Service;
using BanDongHo.Models.ViewModel;
using BanDongHo.Common;
using BotDetect.Web.Mvc;

namespace BanDongHo.Controllers
{
    public class AccountController : Controller
    {
        IRegisterService registerService;
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel register = new RegisterViewModel();
            ViewBag.MessageRegister = "";
            return View(register);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterViewModel register)
        {
            ViewBag.MessageRegister = "";
            // Kiểm tra dữ liệu
            registerService = new RegisterService();
            if(ModelState.IsValid)
            {
                if (registerService.isExistAccount(register.Account))
                {
                    register.Account = "";
                    ViewBag.MessageRegister += "Tài khoản đã tồn tại !";
                    return View(register);
                }
                if (!registerService.isValidPassword(register.Password))
                {
                    register.Password = "";
                    ViewBag.MessageRegister += "Mật khẩu không đúng định dạng!";
                    return View(register);
                }
                // thêm dữ liệu từ form vào model, sau đó từ model xuống cơ sở dữ liệu
                registerService.RegisterAccount(register);

                // Gửi mail cho người dùng khi đã đăng ký thành công
                string content = System.IO.File.ReadAllText(Server.MapPath("/Views/Others/newuser.html"));
                content = content.Replace("{{Account}}", register.FirstName + " " + register.LastName);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap");

                MailHelper.SendMail(register.Email, "Đăng ký thành công", content);

                ViewData["SuccessMsg"] = "Đăng ký thành công";
                register = new RegisterViewModel();
                MvcCaptcha.ResetCaptcha("registerCaptcha");
            }
           
            return View(register);
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            ViewBag.MessageLogin = "";
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            ViewBag.MessageLogin = "";
            if (ModelState.IsValid)
            {
                LoginService loginService = new LoginService();
                var result = loginService.Login(loginViewModel.UserName, Encryptor.MD5Hash(loginViewModel.PassWord));
                if(result == 1)
                {
                    var user = loginService.GetById(loginViewModel.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.TENDN;
                    userSession.UserID = user.MATK;
                    Session.Add(CommonConstands.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if(result == 0)
                {
                    ViewBag.MessageLogin += "Tài khoản không tồn tại";
                }
                else if (result == -1)
                {
                    ViewBag.MessageLogin += "Tài khoản đang bị khóa";
                }
                else if (result == -2)
                {
                    ViewBag.MessageLogin += "Mật khẩu không đúng";
                }
                else
                {
                    ViewBag.MessageLogin += "Đăng nhập không thành công";
                }
            }
            return View(loginViewModel);
        }

        [ChildActionOnly]
        public ActionResult UserMenu()
        {
            return PartialView();
        }
    }
}