using BanDongHo.Areas.Admin.Models;
using BanDongHo.Common;
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
    }
}