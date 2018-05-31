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
    public class PromotionController : Controller
    {
        PromotionService promotionService = new PromotionService();
        // GET: Admin/Promotion
        public ActionResult Index()
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            return View(promotionService.getAllPromotion());
        }


        [HttpGet]
        public ActionResult Create()
        {
            PromotionViewModel promotionViewModel = new PromotionViewModel();
            promotionViewModel.MAKM = newMAKM(promotionService.getLastRecord());
            return View(promotionViewModel);
        }

        [HttpPost]
        public ActionResult Create(PromotionViewModel km)
        {
            ViewBag.message = "";
            if (ModelState.IsValid)
            {
                KHUYENMAI khuyenmai = new KHUYENMAI();
                khuyenmai.MAKM = km.MAKM;
                khuyenmai.TENKM = km.TENKM;
                khuyenmai.NGAYBD = km.NGAYBD;
                khuyenmai.NGAYKT = km.NGAYKT;

                if (promotionService.addPromotion(khuyenmai))
                {
                    ViewBag.message = "Thêm mới khuyến mãi thành công";
                    km = new PromotionViewModel();
                    km.MAKM = newMAKM(promotionService.getLastRecord());
                    return View(km);
                }
                else
                {
                    ViewBag.message = "Thêm mới khuyến mãi thất bại";
                }
            }
            return View(km);
        }

        [HttpGet]
        public ActionResult Update(string makm)
        {
            PromotionViewModel promotionViewModel = new PromotionViewModel();
            var km = promotionService.getPromotionById(makm);
            promotionViewModel.MAKM = km.MAKM;
            promotionViewModel.TENKM = km.TENKM;
            promotionViewModel.NGAYBD = (DateTime)km.NGAYBD;
            promotionViewModel.NGAYKT = (DateTime)km.NGAYKT;
            return View(promotionViewModel);
        }

        [HttpPost]
        public ActionResult Update(PromotionViewModel km)
        {
            
            if (ModelState.IsValid)
            {
                KHUYENMAI khuyenmai = new KHUYENMAI();
                khuyenmai.MAKM = km.MAKM;
                khuyenmai.TENKM = km.TENKM;
                khuyenmai.NGAYBD = km.NGAYBD;
                khuyenmai.NGAYKT = km.NGAYKT;

                if (promotionService.updatePromotion(khuyenmai))
                {
                    ViewBag.message = "Sửa khuyến mãi thành công";
                    return RedirectToAction("Index","Promotion", promotionService.getAllPromotion());
                }
                else
                {
                    ViewBag.message = "Sửa khuyến mãi thất bại";
                }
              
            }
            return View(km);
        }

        public ActionResult Delete(string makm)
        {
            return Json(new { result = promotionService.deletePromotion(makm) });


        }

        public string newMAKM(string lastMAKM)
        {
            string res = "KM00001";
            if (String.Compare(lastMAKM, "", false) != 0)
            {              
                int tam = Int32.Parse(lastMAKM.Substring(2)) + 1;
                string rs = tam.ToString();
                while (rs.Length < 5)
                {
                    rs = "0" + rs;
                }
                                           
                res = "KM" + rs;
                
            }
            return res;
        }
    }
}