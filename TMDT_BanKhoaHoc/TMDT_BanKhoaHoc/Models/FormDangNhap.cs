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
        [Display(Name = "Tài khoản")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
    }
}