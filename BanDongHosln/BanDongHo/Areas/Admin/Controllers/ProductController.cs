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
            var viewModel = new ProductPagerViewModel
            {
                Products = productService.loadProduct(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return View(viewModel);
        }

        public ActionResult Product(int? page)
        {
            var pager = new Pager(productService.getTotalRecord(), page);
            var viewModel = new ProductPagerViewModel
            {
                Products = productService.loadProduct(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return View(viewModel);

        }

        public ActionResult Create()
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            ProductViewModel productviewmodel = new ProductViewModel();
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            return View(productviewmodel);
        }


        public ActionResult Detail(int masp)
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            return View(productService.getProductById(masp));
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel sanpham)
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            if (ModelState.IsValid)
            {
                SANPHAM sp = new SANPHAM();
                sp.TENSP = sanpham.TENSP;
                sp.SOLUONG = sanpham.SOLUONG;
                sp.MATH = sanpham.MATH;
                sp.MOTA = sanpham.MOTA;
                sp.DANHGIA = sanpham.DANHGIA;
                sp.DONGIA = sanpham.DONGIA;
                sp.MALOAISP = sanpham.MALOAISP;
                sp.HINHLON = sanpham.HINHLON;
                sp.HINHNHO = sanpham.HINHNHO;
                return View(sanpham);

            }
            else
            {
                return View(sanpham);
            }
        }

        [HttpGet]
        public ActionResult Update(int masp)
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();

            ProductViewModel sp = new ProductViewModel();
            var sanpham = productService.getProductById(masp);

            sp.TENSP = sanpham.TENSP;
            sp.SOLUONG = (int) sanpham.SOLUONG;
            sp.MATH = (int) sanpham.MATH;
            sp.MOTA = sanpham.MOTA;
            sp.DANHGIA = sanpham.DANHGIA;
            sp.DONGIA = (double)sanpham.DONGIA;
            sp.MALOAISP = sanpham.MALOAISP;
            sp.HINHLON = sanpham.HINHLON;
            sp.HINHNHO = sanpham.HINHNHO;

            return View(sp);
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel sanpham)
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();

            if (ModelState.IsValid)
            {
                SANPHAM sp = new SANPHAM();

                sp.TENSP = sanpham.TENSP;
                sp.SOLUONG = (int)sanpham.SOLUONG;
                sp.MATH = (int) sanpham.MATH;
                sp.MOTA = sanpham.MOTA;
                sp.DANHGIA = sanpham.DANHGIA;
                sp.DONGIA = (double)sanpham.DONGIA;
                sp.MALOAISP = sanpham.MALOAISP;
                sp.HINHLON = sanpham.HINHLON;
                sp.HINHNHO = sanpham.HINHNHO;

                productService.updateProduct(sp);
                
                return View(sanpham);

            }
            else
            {
                return View(sanpham);
            }

            //var spx = productService.getProductById(sp.MASP);
            //if (spx != null)
            //{
            //    var result = new
            //    {
            //        sp.TENSP,
            //        sp.SOLUONG,
            //        sp.THUONGHIEU.TENTH,
            //        sp.LOAISANPHAM.TENLOAISP,
            //        sp.DONGIA
            //    };
            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return null;
            //}
        }

        public ActionResult deleteProduct(int masp)
        {
            return Json(new { result = productService.deleteProduct(masp) });
        }

    }
}