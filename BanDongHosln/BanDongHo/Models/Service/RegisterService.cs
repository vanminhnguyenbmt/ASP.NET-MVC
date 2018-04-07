using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Models.ViewModel;
using BanDongHo.Domain.DataContext;
using System.Text.RegularExpressions;

namespace BanDongHo.Models.Service
{
    public class RegisterService : IRegisterService
    {
        const string ID_DEFAULT = "KH00001";
        const string TK_DEFAULT = "TK00001";
        public bool isExistAccount(string account)
        {
            // get tk => tk.TENDN == account
            // if tk == null return true ? falsea
            BANDONGHOEntities db = new BANDONGHOEntities();
            TAIKHOAN taikhoan = (from tk in db.TAIKHOANs
                                 where tk.TENDN.Equals(account)
                                 select tk).SingleOrDefault();
            if(taikhoan!=null)
            {
                return true;
            }
            return false;
        }

        public bool isValidPassword(string password)
        {
            return Regex.IsMatch(password, "^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$");
        }

        public void GetMaKH(string MAKH)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            // Lấy mã khách hàng lớn nhất 
            KHACHHANG kh = (from KH in db.KHACHHANGs
                            orderby KH.MAKH
                            select KH).SingleOrDefault();
            if (kh == null)
            {
                MAKH = ID_DEFAULT;
            }
            else
            {
                // cắt 2 ký tự đầu
                int numberID;
                if (!Int32.TryParse(MAKH.Substring(2), out numberID))
                {
                    MAKH = ID_DEFAULT;
                }
                else
                {
                    numberID++;
                    string newID = "KH";
                    for (int i = 0; i < 5 - numberID.ToString().Length; i++)
                    {
                        newID += "0";
                    }
                    newID += numberID.ToString();
                    MAKH = newID;
                }
            }
        }

        public void GetMaTK(string MATK)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            // Lấy mã tài khoản lớn nhất 
            TAIKHOAN tk = (from TK in db.TAIKHOANs
                            orderby TK.MATK
                            select TK).SingleOrDefault();
            if (tk == null)
            {
                MATK = TK_DEFAULT;
            }
            else
            {
                // cắt 2 ký tự đầu
                int numberID;
                if (!Int32.TryParse(MATK.Substring(2), out numberID))
                {
                    MATK = TK_DEFAULT;
                }
                else
                {
                    numberID++;
                    string newID = "TK";
                    for (int i = 0; i < 5 - numberID.ToString().Length; i++)
                    {
                        newID += "0";
                    }
                    newID += numberID.ToString();
                    MATK = newID;
                }
            }
        }

        public void RegisterAccount(RegisterViewModel register)
        {
            string MAKH = "", MATK = "";
            BANDONGHOEntities db = new BANDONGHOEntities();
            // Lấy mã tài khoản lớn nhất
            GetMaTK(MATK);

            // Lấy mã khách hàng lớn nhất 
            GetMaKH(MAKH);

            //Tạo mới tài khoản
            TAIKHOAN account = new TAIKHOAN { MATK = int.Parse(MATK), TENDN = register.Account, MATKHAU = register.Password, MALOAITK = "LK00002"};
            // Tạo mới khách hàng
            KHACHHANG customer = new KHACHHANG { MAKH = int.Parse(MAKH), TENKH = register.FirstName + register.LastName, DIACHI = register.Address, EMAIL = register.Email, SDT = register.Phone, GIOITINH = register.Sex};
            // Thêm khách hàng và tài khoản vào db
            db.TAIKHOANs.Add(account);
            db.KHACHHANGs.Add(customer);
            db.SaveChanges();
        }
    }
}