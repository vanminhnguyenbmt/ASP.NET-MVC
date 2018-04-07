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
        public HomeController()
        {
            
        }
        public ActionResult Index()
        {
            HomePageViewModel HomePageVM = new HomePageViewModel();
            HomePageVM.ProductsSelling = ProductService.GetListProductsSelling();
            HomePageVM.NewProducts = ProductService.GetListNewProducts().Take(8);
            return View(HomePageVM);
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