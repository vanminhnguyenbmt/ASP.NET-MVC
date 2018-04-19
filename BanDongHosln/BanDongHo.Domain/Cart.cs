

namespace BanDongHo.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Cart
    {
        private List<CartItem> Products = new List<CartItem>();

        // phương thức thêm sản phẩm
        public void AddProduct(SANPHAM sanpham, int soluong)
        {
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
                Products.Add(new CartItem { Product = sanpham, Quantity = soluong });
            }
        }
        // phương thức xóa sản phẩm
        public void RemoveProduct(SANPHAM sanpham)
        {
            var item = (from i in Products
                        where i.Product.MASP == sanpham.MASP
                        select i).SingleOrDefault();
            // linq
            // Products.Where(i=>i.Product.MASP==sanpham.MASP).SingleOrDefault();
            if (item != null)
            {
                Products.Remove(item);
            }
        }
        // phương thức cập nhật
        public void UpdateProduct(SANPHAM sanpham, int soluong)
        {
            var item = (from i in Products
                        where i.Product.MASP == sanpham.MASP
                        select i).SingleOrDefault();
            // linq
            // Products.Where(i=>i.Product.MASP==sanpham.MASP).SingleOrDefault();
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
            return Products.Sum(pi => pi.Quantity * pi.Product.DONGIA.Value);
        }
    }
}
