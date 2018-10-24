using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TMDT_BanKhoaHoc.Models
{
    public class FormDangNhap
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [StringLength(50,MinimumLength =6,ErrorMessage = "Tài khoản phải có từ 6-50 ký tự")]
        [Display(Name = "Tài khoản")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có từ 6-50 ký tự")]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
    }
}