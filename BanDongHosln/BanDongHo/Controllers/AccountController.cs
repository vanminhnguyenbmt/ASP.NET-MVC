using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models.Service;
using BanDongHo.Models.ViewModel;

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
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            ViewBag.MessageRegister = "";
            // Kiểm tra dữ liệu
            registerService = new RegisterService();
            if (registerService.isExistAccount(register.Account))
            {
                register.Account = "";
                ViewBag.MessageRegister += "Tài khoản đã tồn tại !";
                return View(register);
            }
            if (!registerService.isValidPassword(register.Password))
            {
                register.Password = "";
                ViewBag.MessageRegister += "Mật khẩu sai định dạng !";
                return View(register);
            }
            registerService.RegisterAccount(register);
            return View(register);
        }
    }
}