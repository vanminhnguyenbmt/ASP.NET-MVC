using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

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
        public static IEnumerable<SANPHAM> LoadProductAll()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.SANPHAMs;
        }
        /// <summary>
        /// Service load sản phẩm nam
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SANPHAM> LoadProductMen()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.SANPHAMs.Where(n=>n.MALOAISP == "LP00001");
        }
        /// <summary>
        /// Service load sản phẩm nữ
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SANPHAM> LoadProductWomen()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.SANPHAMs.Where(n => n.MALOAISP == "LP00002");
        }
    }
}