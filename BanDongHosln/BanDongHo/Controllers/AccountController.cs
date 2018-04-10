using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models.Service;
using BanDongHo.Models.ViewModel;
using BanDongHo.Common;

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
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "Home/Account");

                MailHelper.SendMail(register.Email, "Đăng ký thành công", content);

                ViewData["SuccessMsg"] = "Đăng ký thành công";
            }
           
            return View(register);
        }

        public ActionResult Login(string returnURL)
        {
            LoginViewModel login = new LoginViewModel();
            ViewBag.ReturnURL = returnURL;
            return View(login);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            return View(login);
        }
    }
}