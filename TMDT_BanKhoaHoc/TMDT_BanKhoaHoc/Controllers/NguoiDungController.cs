using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_BanKhoaHoc.Models;

namespace TMDT_BanKhoaHoc.Controllers
{
    public class NguoiDungController : Control
    {

        // GET: NguoiDung/Details/5
        public ActionResult Index()
        {
            var khoahocs = db.KHOAHOCs;

            if(khoahocs.Count() == 0)
            {
                return View();
            }

            return View(khoahocs);
        }

        public ActionResult DangNhap()
        {
            if(Session[SesHocVien] == null)
            {
                return View();
            }
            return Redirect("/");
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(FormDangNhap form)
        {
            if(ValidateRequest)
            {
                if (Session[SesHocVien] != null)
                {
                    return Redirect("/");
                }

                HOCVIEN hv = db.HOCVIENs.SingleOrDefault(n => n.Taikhoan == form.TaiKhoan && n.Matkhau == form.MatKhau);

                if (hv == null)
                {
                    form.MatKhau = "";
                    ViewData["loi"] = "Tài khoản hoặc mật khẩu không đúng";
                    return View(form);
                }

                return Redirect("/");
            }
            return View(form);
        }

        // GET: NguoiDung/Create
        public ActionResult DangKy()
        {
            if(Session[SesHocVien] != null)
            {
                return Redirect("/");
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult DangKy(HOCVIEN hocvien)
        {
            try
            {
                if(Session[SesHocVien] != null)
                {
                    return Redirect("/");
                }

                HOCVIEN hv = db.HOCVIENs.SingleOrDefault(n => n.Taikhoan == hocvien.Taikhoan);

                if(hv != null)
                {
                    ViewData["taikhoan"] = "Tài khoản đã được sử dụng";
                    return View(hocvien);
                }

                if(hocvien.Email.IndexOf('@') > 0)
                {
                    string[] a = hocvien.Email.Split('@');
                    if(a[1].IndexOf('.') < 1)
                    {
                        ViewData["email"] = "Email không hợp lệ";
                        return View(hocvien);
                    }
                } else
                {
                    ViewData["email"] = "Email không hợp lệ";
                    return View(hocvien);
                }
                    
                db.HOCVIENs.InsertOnSubmit(hocvien);

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }
    }
}
