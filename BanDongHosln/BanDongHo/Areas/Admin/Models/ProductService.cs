using BanDongHo.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductService
    {
        public IEnumerable<SANPHAM> getAllProduct()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.SANPHAMs;
        }

        public int getTotalRecord()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Count();
        }

        public SANPHAM getProductById(int masp)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.SANPHAMs.Find(masp);
        }

        public bool addProduct(SANPHAM sp)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            try
            {
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public bool updateProduct(SANPHAM sp)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            try
            {
                var result = db.SANPHAMs.Find(sp.MASP);
                if(result != null)
                {
                    result.TENSP = sp.TENSP;
                    result.SOLUONG = sp.SOLUONG;
                    result.MATH = sp.MATH;
                    result.MOTA = sp.MOTA;
                    result.DONGIA = sp.DONGIA;
                    result.MALOAISP = sp.MALOAISP;
                    result.HINHLON = sp.HINHLON;
                    result.HINHNHO = sp.HINHNHO;
                    result.DANHGIA = sp.DANHGIA;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deleteProduct(int masp)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            try {
                db.SANPHAMs.Remove(db.SANPHAMs.Find(masp));
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            

        }

     
        public IEnumerable<SANPHAM> loadProduct(int pageIndex, int pageSize)
        {
            IEnumerable<SANPHAM> ListProduct = null;
            BANDONGHOEntities db = new BANDONGHOEntities();

            ListProduct = (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return ListProduct;
        }

        public IEnumerable<LOAISANPHAM> getLoaiSanPham()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.LOAISANPHAMs;
        }

        public IEnumerable<THUONGHIEU> getThuongHieu()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.THUONGHIEUx;
        }
    }
}