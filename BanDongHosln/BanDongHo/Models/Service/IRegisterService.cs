using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanDongHo.Models.ViewModel;

namespace BanDongHo.Models.Service
{
    interface IRegisterService
    {
        /// <summary>
        /// Service kiểm tra tài khoản tồn tại chưa
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool isExistAccount(string account);

        /// <summary>
        /// Service kiểm tra mật khẩu hợp lệ không
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool isValidPassword(string password);
        void RegisterAccount(RegisterViewModel register);
    }
}
