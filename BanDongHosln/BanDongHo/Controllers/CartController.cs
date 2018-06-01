using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDongHo.Models.Models;
using BanDongHo.Domain.DataContext;
using System.Globalization;
using BanDongHo.Models.Service;

namespace BanDongHo.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            /* Tạo mới một giỏ hàng nếu nó bằng null
               Giỏ hàng được tạo khi người dùng nhấp vào giỏ hàng hoặc 
               người dùng mua một sản phẩm  */
            // Giỏ hàng được quản lý bằng một biến Session
            Cart cart = Session["Cart"] as Cart;
            // Nếu chưa có giỏ hàng thì tạo mới một giỏ hàng
            if (cart == null)
            {
                cart = new Cart();
            }
            // gán giỏ hàng cho biến session[cart]
            Session["Cart"] = cart;
            // Trả giỏ hàng ra giao diện 
            return View(Session["Cart"] as Cart);
        }

        public ActionResult AddProduct(int id,int sl=1)
        {
            /*
             * Thêm một sản phẩm vào giỏ hàng
             * được gọi khi người dùng chọn một 
             * sản phẩm từ trang chi tiết
             */
             // Lấy biến giỏ hàng từ session
            Cart cart = Session["Cart"] as Cart;
            // Nếu chưa có thì tạo mới giỏ hàng
            if (cart == null)
            {
                cart = new Cart();
            }
            // Thêm sản phẩm vào giỏ hàng
            cart.AddProduct(id, sl);
            // Gán lại giỏ hàng cho session
            Session["Cart"] = cart;
            // Trở về giao diện index
            return RedirectToAction("Index");
        }

        public string DeleteProduct(int id)
        {
            /*
             * Xóa một sản phẩm khi người dùng chọn button delete
             * trong màn hình giỏ hàng. Khi xóa sản phẩm bị loại 
             * ra khỏi giỏ hàng.
             */

            // lấy giỏ hàng từ session
            Cart cart = Session["Cart"] as Cart;
            // Nếu chưa có thì trả về total = 0;
            if(cart==null)
            {
                return "Total:";
            }
            // Xóa sản phẩm đi
            cart.RemoveProduct(id);
            // gán lại giỏ hàng cho session
            Session["Cart"] = cart;
            // Trả về chuỗi total đã định dạng tiền mặt việt nam
            return "Total: " + cart.TotalMoney().ToString("0,0");
        }

        public string UpdateProduct(int id,int sl)
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                return "Total:";
            }
            cart.UpdateProduct(id,sl);
            Session["Cart"] = cart;
            string res="Total: "+ cart.TotalMoney().ToString("0,0");
            return res;
        }

        public ActionResult Checkout()
        {
            /*
             * Kiểm tra số lượng của mỗi sản phẩm trên giỏ có đủ với số lượng 
             * trong kho hàng hay không?
             */ 
            Cart cart = (Cart)Session["Cart"];
            bool isFalse = false;
            foreach(var item in cart.GetList())
            {
                if(!CartService.CheckNumberProduct(item.Product.MASP,item.Quantity))
                {
                    isFalse = true;
                }
            }
            if(!isFalse)
            {
                return RedirectToAction("Index", "CusInfo");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [ChildActionOnly]
        public ActionResult CartMenu()
        {
            return PartialView();
        }
    }
}