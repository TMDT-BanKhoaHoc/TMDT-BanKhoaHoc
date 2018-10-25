using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_BanKhoaHoc.Models;

namespace TMDT_BanKhoaHoc.Controllers
{
    public class GiangVienController : Control
    {
        // GET: GiangVien
        public ActionResult Index()
        {
            return View(db.KHOAHOCs.ToList());
        }

        //Dang nhap
        [HttpGet]
        public ActionResult DangNhapGV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhapGV(FormCollection collection)
        {
            var acc = collection["account"];
            var mk = collection["pass"];
            if (String.IsNullOrEmpty(acc))
            {
                ViewData["loi1"] = "Không được bỏ trống tài khoản";
            }
            else if (String.IsNullOrEmpty(mk))
            {
                ViewData["loi1"] = "Không được bỏ trống mật khẩu !!";
            }
            else
            {
                GIANGVIEN gv = db.GIANGVIENs.SingleOrDefault(n => n.TaiKhoan == acc && n.MatKhau == mk);
                if (gv != null)
                {
                    Session[SesAdmin] = gv;
                    return RedirectToAction("Index", "GiangVien");
                }
                else
                    ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng";
            }
            return View();
        }

        //Quan li khoa hoc
         [HttpGet]
        public ActionResult Addkh()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Addkh(KHOAHOC kh)
        {
            if (ModelState.IsValid)
            {
                db.KHOAHOCs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("KH");
            }
            return View(kh);
        }

    }
}