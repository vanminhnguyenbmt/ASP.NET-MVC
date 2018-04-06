using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models.Service;
using BanDongHo.Models.ViewModel;

namespace BanDongHo.Controllers
{
    public class HomeController : Controller
    {
        IRegisterSercive registerservice;
        public HomeController()
        {
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            Register register = new Register();
            ViewBag.MessageRegister = "";
            return View(register);
        }
        [HttpPost]
        public ActionResult Register(Register register)
        {
            ViewBag.MessageRegister = "";
            // Kiểm tra dữ liệu
            registerservice = new RegisterService();
            if (registerservice.isExistAccount(register.Account))
            {
                register.Account = "";
                ViewBag.MessageRegister += "Tài khoản đã tồn tại!";
                return View(register);
            }
            if (!registerservice.isPasswordAccount(register.Password))
            {
                register.Password = "";
                ViewBag.MessageRegister += "Mật khẩu sai định dạng!";
                return View(register);
            }
            return View(register);
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}