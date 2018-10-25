using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TMDT_BanKhoaHoc.Models
{
    public class FormDoiMatKhau
    {
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Chưa nhập mật khẩu cũ")]
        public string MatKhauCu { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Chưa nhập mật khẩu mới")]
        [StringLength(50,MinimumLength = 6, ErrorMessage = "Mật khẩu mới phải có tử 6-50 ký tự")]
        public string MatKhauMoi { get; set; }
    }
}