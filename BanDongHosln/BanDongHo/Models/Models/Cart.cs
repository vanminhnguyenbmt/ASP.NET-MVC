using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;
using BanDongHo.Models.Service;

namespace BanDongHo.Models.Models
{
    public class Cart
    {
        private List<CartItem> Products = new List<CartItem>();
        public List<string> Message {
            get
            {
                List<string> res = new List<string>(); 
                foreach(var item in Products)
                {
                    if(!CartService.CheckNumberProduct(item.Product.MASP,item.Quantity))
                    {
                        string mes = "Sản phẩm " + item.Product.TENSP + " không đủ số lượng";
                        res.Add(mes);
                    }
                }
                return res;
            }
                }
        // phương thức thêm sản phẩm
        public void AddProduct(int id, int soluong)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            // Lấy sản phẩm từ CSDL
            var sanpham = db.SANPHAMs.Find(id);
            if (sanpham == null)
                return;
            // Tìm sản phẩm trong giỏ hàng
            // xem đã được add vào chưa
            var item = (from i in Products
                        where i.Product.MASP == sanpham.MASP
                        select i).SingleOrDefault();
            // nếu sp có trong cart rồi thì cập nhật số lượng
            if (item != null)
            {
                item.Quantity += soluong;
            }
            // nếu sản phẩm chưa có thì thêm mới
            else
            {
                // lấy khuyến mãi
                int Promotion = PromotionService.GetPromotion(id);
                Products.Add(new CartItem { Product = sanpham, Quantity = soluong,Promotion = Promotion });
            }
        }
        // phương thức xóa sản phẩm
        public void RemoveProduct(int id)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            // Tìm sản phẩm từ CSDL
            var sanpham = db.SANPHAMs.Find(id);
            if (sanpham == null)
                return;
            // tìm xem sản phẩm đó đã có trong giỏ hàng chưa
            var item = (from i in Products
                        where i.Product.MASP == sanpham.MASP
                        select i).SingleOrDefault();
            // Nếu có rồi thì remove sản phẩm đi
            if (item != null)
            {
                Products.Remove(item);
            }
        }
        // phương thức cập nhật
        public void UpdateProduct(int id, int soluong)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            // Tìm sản phẩm từ CSDL
            var sanpham = db.SANPHAMs.Find(id);
            if (sanpham == null)
                return;
            // Tìm sản phẩm xem có trong giỏ hàng chưa
            var item = (from i in Products
                        where i.Product.MASP == sanpham.MASP
                        select i).SingleOrDefault();
            // nếu có thì cập nhật số lượng
            if (item != null)
            {
                item.Quantity = soluong;
            }
        }
        // Phương thức lấy ra danh sách
        public List<CartItem> GetList()
        {
            return Products;
        }
        // phương thức tính tổng tiền
        public double TotalMoney()
        {
            if(Products.Count==0)
            {
                return 0;
            }
            return Products.Sum(pi => pi.Quantity * (pi.Product.DONGIA.Value*(float)(100 - pi.Promotion)/100));
        }
    }
}