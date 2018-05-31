using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Areas.Admin.Models;
using BanDongHo.Common;

namespace BanDongHo.Areas.Admin.Controllers
{
    
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            return View(orderService.getAllOrder());
        }
    }
}