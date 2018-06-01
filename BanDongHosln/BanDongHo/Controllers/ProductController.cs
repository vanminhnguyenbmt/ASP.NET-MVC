using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models.ViewModel;
using BanDongHo.Models.Service;

namespace BanDongHo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
       
        public ActionResult Index(string cateKey, int page = 1)
        {
            int pageSize = 8;
            ViewData["pageSize"] = pageSize;
            ProductCategoryViewModel prctViewModel = new ProductCategoryViewModel();

            //lấy ra danh sách sản phẩm theo loại
            if(cateKey=="tat-ca")
            {
                prctViewModel.ListProductCategory = ProductCategoryService.LoadProductAll();
            }
            else
            {
                if(cateKey == "dong-ho-nam")
                {
                    prctViewModel.ListProductCategory = ProductCategoryService.LoadProductMen();
                }
                else
                {
                    prctViewModel.ListProductCategory = ProductCategoryService.LoadProductWomen();
                }
            }

            int totalRecord = prctViewModel.ListProductCategory.Count;
            prctViewModel.CateKey = cateKey;
            prctViewModel.Index = page;
            prctViewModel.TotalPage= (int)(Math.Ceiling(((double)totalRecord / pageSize)));

            return View(prctViewModel);
        }
    }
}