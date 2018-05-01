using BanDongHo.Areas.Admin.Models;
using BanDongHo.Common;
using BanDongHo.Domain.DataContext;
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

        public ActionResult Index(int? page)
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }

            var pager = new Pager(productService.getTotalRecord(), page);
            var viewModel = new ProductViewModel
            {
                Products = productService.loadProduct(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return View(viewModel);
        }

        public ActionResult Product(int? page)
        {
            var pager = new Pager(productService.getTotalRecord(), page);
            var viewModel = new ProductViewModel
            {
                Products = productService.loadProduct(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return View(viewModel);

        }

        public ActionResult Create()
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            return View();
        }

        public ActionResult Update(int masp)
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            return View(productService.getProductById(masp));
        }

        public ActionResult Detail(int masp)
        {
            return View(productService.getProductById(masp));
        }


        public ActionResult updateProduct(SANPHAM sanpham)
        {
            productService.updateProduct(sanpham);
            var sp = productService.getProductById(sanpham.MASP);

            if (sp != null)
            {
                var result = new
                {
                    sp.TENSP,
                    sp.SOLUONG,
                    sp.THUONGHIEU.TENTH,
                    sp.LOAISANPHAM.TENLOAISP,
                    sp.DONGIA
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
            
            
        }

        public ActionResult deleteProduct(int masp)
        {
            return Json(new { result = productService.deleteProduct(masp) });
        }

        public ActionResult addProduct(SANPHAM sanpham)
        {         
            return Json(new { result = productService.addProduct(sanpham) });
        }
    }
}