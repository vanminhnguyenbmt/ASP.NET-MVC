using BanDongHo.Areas.Admin.Models;
using BanDongHo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanDongHo.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {

        ProductService productService = new ProductService();
        // GET: Admin/Product

        [HttpGet]
        public ActionResult Product()
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            return View(productService.getAllProduct());
         
        }


        [HttpPost]
        public ActionResult Create()
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            return View();
        }

        public ActionResult Detail(int masp)
        {
            return View(productService.getProductById(masp));
        }
    }
}