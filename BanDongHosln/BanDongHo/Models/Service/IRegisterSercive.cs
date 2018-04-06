using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanDongHo.Models.ViewModel;
namespace BanDongHo.Models.Service
{
    interface IRegisterSercive
    {
        /// <summary>
        /// Dịch vụ kiểm tra tài khoảng đã có hay chưa
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool isExistAccount(string account);

        /// <summary>
        /// Dịch vụ kiểm tra thông tin mật khẩu có hợp lệ
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool isPasswordAccount(string password);

        /// <summary>
        /// Đăng ký tài khoảng cho server (database)
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        bool RegisterAccount(Register register);

    }
}
