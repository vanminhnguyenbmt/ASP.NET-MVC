using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;
using BanDongHo.Models.ViewModel;

namespace BanDongHo.Models.Service
{
    public class ProductCategoryService
    {
        /// <summary>
        /// category[donghonam, donghonu, null], pageIndex: số trang hiện tại, pageSize: số phần tử trong 1 trang
        /// </summary>
        /// <param name="category"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<SANPHAM> LoadProductCategory(string category, ref int totalRecord, int pageIndex = 1, int pageSize = 8)
        {
            IEnumerable<SANPHAM> ListProductCategory = null;
            BANDONGHOEntities db = new BANDONGHOEntities();
            if (category == "tat-ca")
            {
                totalRecord = (from sp in db.SANPHAMs
                               orderby sp.MASP descending
                               select sp).Count();
                try
                {
                    ListProductCategory = (from sp in db.SANPHAMs
                                           orderby sp.MASP descending
                                           select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                catch (Exception e)
                {

                }
            }
            if (category == "dong-ho-nam")
            {
                totalRecord = (from sp in db.SANPHAMs
                               where sp.MALOAISP == "LP00001"
                               orderby sp.MASP descending
                               select sp).Count();
                try
                {
                    ListProductCategory = (from sp in db.SANPHAMs
                                           where sp.MALOAISP == "LP00001"
                                           orderby sp.MASP descending
                                           select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                catch (Exception e)
                {

                }
            }
            if (category == "dong-ho-nu")
            {
                totalRecord = (from sp in db.SANPHAMs
                               where sp.MALOAISP == "LP00002"
                               orderby sp.MASP descending
                               select sp).Count();
                try
                {
                    ListProductCategory = (from sp in db.SANPHAMs
                                           where sp.MALOAISP == "LP00002"
                                           orderby sp.MASP descending
                                           select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                catch (Exception e)
                {

                }
            }
            return ListProductCategory;
        }

        /// <summary>
        /// Service load tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        public static List<ProductViewModel> LoadProductAll()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            List<ProductViewModel> result = new List<ProductViewModel>();
            IEnumerable<SANPHAM> LoadProductAll = new List<SANPHAM>();
            LoadProductAll = (from sp in db.SANPHAMs
                              orderby sp.MASP descending
                              select sp);
            // Lấy promotion của sản phẩm
            foreach (SANPHAM sp in LoadProductAll)
            {
                int Promotion = PromotionService.GetPromotion(sp.MASP);
                ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
                result.Add(productViewModel);
            }
            return result;
        }
        /// <summary>
        /// Service load sản phẩm nam
        /// </summary>
        /// <returns></returns>
        public static List<ProductViewModel> LoadProductMen()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            List<ProductViewModel> result = new List<ProductViewModel>();
            IEnumerable<SANPHAM> LoadProductMen = new List<SANPHAM>();

            LoadProductMen = (from sp in db.SANPHAMs
                              where sp.MALOAISP == "LP00001"
                              orderby sp.MASP descending
                              select sp);

            // Lấy promotion của sản phẩm
            foreach (SANPHAM sp in LoadProductMen)
            {
                int Promotion = PromotionService.GetPromotion(sp.MASP);
                ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
                result.Add(productViewModel);
            }
            return result;
        }
        /// <summary>
        /// Service load sản phẩm nữ
        /// </summary>
        /// <returns></returns>
        public static List<ProductViewModel> LoadProductWomen()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            List<ProductViewModel> result = new List<ProductViewModel>();
            IEnumerable<SANPHAM> LoadProductWomen = new List<SANPHAM>();

            LoadProductWomen = (from sp in db.SANPHAMs
                              where sp.MALOAISP == "LP00002"
                              orderby sp.MASP descending
                              select sp);

            // Lấy promotion của sản phẩm
            foreach (SANPHAM sp in LoadProductWomen)
            {
                int Promotion = PromotionService.GetPromotion(sp.MASP);
                ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
                result.Add(productViewModel);
            }
            return result;
        }
    }
}