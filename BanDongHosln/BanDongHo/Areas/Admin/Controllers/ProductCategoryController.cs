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
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        ProductCategoryService productCategoryService = new ProductCategoryService();
       
        [HttpGet]
        public ActionResult Index()
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }

            return View(productCategoryService.getAllProductCategory());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel();
            productCategoryViewModel.MALOAISP = newMALOAISP(productCategoryService.getLastRecord());
            return View(productCategoryViewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductCategoryViewModel lsp)
        {
            ViewBag.message = "";
            if (ModelState.IsValid)
            {
                LOAISANPHAM loaisp = new LOAISANPHAM();
                loaisp.MALOAISP = lsp.MALOAISP;
                loaisp.TENLOAISP = lsp.TENLOAISP;

                if (productCategoryService.addProductCategory(loaisp))
                {
                    ViewBag.message = "Thêm mới loại sản phẩm thành công";
                    lsp = new ProductCategoryViewModel();
                    lsp.MALOAISP = newMALOAISP(productCategoryService.getLastRecord());
                    return View(lsp);
                }
                else
                {
                    ViewBag.message = "Thêm mới loại sản phẩm thất bại";
                }               
            }
            return View(lsp);
        }

        [HttpGet]
        public ActionResult Update(string malsp)
        {
            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel();
            var res = productCategoryService.getProductCategoryById(malsp);
            productCategoryViewModel.MALOAISP = res.MALOAISP;
            productCategoryViewModel.TENLOAISP = res.TENLOAISP;
            return View(productCategoryViewModel);
        }

        [HttpPost]
        public ActionResult Update(ProductCategoryViewModel lsp)
        {
            if (ModelState.IsValid)
            {
                LOAISANPHAM loaisp = new LOAISANPHAM();
                loaisp.MALOAISP = lsp.MALOAISP;
                loaisp.TENLOAISP = lsp.TENLOAISP;

                if (productCategoryService.updateProductCategory(loaisp))
                {
                    return RedirectToAction("Index", "ProductCategory", productCategoryService.getAllProductCategory());
                }
                
            }
            return View(lsp);
        }

        public ActionResult Delete(string malsp)
        {
            return Json(new { result = productCategoryService.deleteProductCategory(malsp)});
           
            
        }

        public string newMALOAISP(string lastMALOAISP)
        {
            string res = "LP00001";
            if (String.Compare(lastMALOAISP, "", false) != 0)
            {
                int tam = Int32.Parse(lastMALOAISP.Substring(2)) + 1;
                string rs = tam.ToString();
                while (rs.Length < 5)
                {
                    rs = "0" + rs;
                }

                res = "LP" + rs;

            }
            return res;
        }
    }
}