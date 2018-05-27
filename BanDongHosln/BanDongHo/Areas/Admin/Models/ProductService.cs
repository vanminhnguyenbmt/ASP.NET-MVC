using BanDongHo.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductService
    {
        BANDONGHOEntities db;
        public ProductService()
        {
            db = new BANDONGHOEntities();
        }
       
        public IEnumerable<SANPHAM> getAllProduct()
        {          
            return db.SANPHAMs;
        }

        public int getTotalRecord()
        {         
            return (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Count();
        }

        public SANPHAM getProductById(int masp)
        {          
            return db.SANPHAMs.Find(masp);
        }

        public bool addProduct(SANPHAM sp)
        {          
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
                  
            ListProduct = (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return ListProduct;
        }

        public IEnumerable<LOAISANPHAM> getLoaiSanPham()
        {           
            return db.LOAISANPHAMs;
        }

        public IEnumerable<THUONGHIEU> getThuongHieu()
        {            
            return db.THUONGHIEUx;
        }
    }
}