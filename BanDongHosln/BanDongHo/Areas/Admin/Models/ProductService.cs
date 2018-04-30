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

        public void addProduct(SANPHAM sp)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            db.SANPHAMs.Add(sp);
            db.SaveChanges();
        }

        public SANPHAM getProductById(int masp)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.SANPHAMs.Find(masp);
        }

        public void deleteProduct(int masp)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            db.SANPHAMs.Remove(db.SANPHAMs.Find(masp));
            db.SaveChanges();

        }

        public int getTotalRecord()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Count();
        }

        public IEnumerable<SANPHAM> loadProduct(int pageIndex, int pageSize = 8)
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