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
            
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
            }
            Session["Cart"] = cart;
            return View(Session["Cart"] as Cart);
        }
        public ActionResult AddProduct(int id,int sl=1)
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
            }
            cart.AddProduct(id, sl);
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }
        public string DeleteProduct(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart==null)
            {
                return "Total:";
            }
            cart.RemoveProduct(id);
            Session["Cart"] = cart;
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
    }
}