using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;
using BanDongHo.Models.ViewModel;

namespace BanDongHo.Models.Service
{
    public class ProductService
    {
        public static List<SANPHAM> GetListProductsSelling()
        {
            List<SANPHAM> ListProductsSelling = null;

            // nghiệp vụ
            // * Nếu ngày lấy danh sách trước ngày 15 hàng tháng thì sẽ lấy danh sách tháng trước
            // * Lấy ra danh sách sản phẩm bán chạy trong tháng đó
            // * Nếu danh sách lấy ra là null thì trả về danh sách 3 sản phẩm mới nhất

            int Month = DateTime.Today.Month;
            int Year = DateTime.Today.Year;
            if (DateTime.Today.Day < 15)
            {
                if (Month - 1 == 0)
                {
                    Year -= 1;
                    Month = 12;
                }
                else
                {
                    Month -= 1;
                }
            }

            BANDONGHOEntities db = new BANDONGHOEntities();
            try
            {
                ListProductsSelling = (from sp in db.SANPHAMs
                                       let totalQuantity = (from ct in db.CHITIETDONHANGs
                                                            join dh in db.DONHANGs on ct.MADH equals dh.MADH
                                                            where sp.MASP == ct.MASP && dh.NGAYDAT.Value.Month == Month && dh.NGAYDAT.Value.Year == Year
                                                            select ct.SOLUONG).Sum()
                                       where totalQuantity > 0
                                       orderby totalQuantity descending
                                       select sp).Take(3).ToList();
            }
            catch (Exception e) { }

            if (ListProductsSelling == null || ListProductsSelling.ToList().Count < 3)
            {
                List<SANPHAM> lstListNewProduct = new List<SANPHAM>();
                foreach (ProductViewModel sp in GetListNewProducts().Take(3))
                {
                    lstListNewProduct.Add(sp.Product);
                }
                return lstListNewProduct;
            }

            List<SANPHAM> lsp = ListProductsSelling.ToList();

            return ListProductsSelling;
        }

        internal static List<ProductViewModel> GetListProductRelative(int idTrademark)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            // Lấy danh sách sản phẩm
            List<SANPHAM> ListProducts = new List<SANPHAM>();
            using (var db = new BANDONGHOEntities())
            {
                ListProducts = (from sp in db.SANPHAMs
                                  where sp.MATH == idTrademark
                                  orderby sp.MASP descending
                                  select sp).ToList();
            }
            foreach(SANPHAM sp in ListProducts)
            {
                int Promotion = PromotionService.GetPromotion(sp.MASP);
                ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
                result.Add(productViewModel);
            }
            return result;
        }

        public static List<ProductViewModel> GetListNewProducts()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            // Lấy danh sách sản phẩm
            List<SANPHAM> ListNewProducts = null;
            using (var db = new BANDONGHOEntities())
            {
                ListNewProducts =( from sp in db.SANPHAMs
                                  orderby sp.MASP descending
                                  select sp).ToList();
            }

            // Lấy promotion của sản phẩm
            foreach (SANPHAM sp in ListNewProducts)
            {
                int Promotion = PromotionService.GetPromotion(sp.MASP);
                ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
                result.Add(productViewModel);
            }

            return result;
        }

        public static SANPHAM Find(int id)
        {

            SANPHAM result = null;
            using (var db = new BANDONGHOEntities())
            {
                result = db.SANPHAMs.Find(id);
            }
            return result;
                
        }


    }
}