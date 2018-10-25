using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_BanKhoaHoc.Models;

namespace TMDT_BanKhoaHoc.Controllers
{
    public class AdminController : Control
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangNhapAD()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhapAD(FormCollection collection)
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
                Admin ad = db.Admins.SingleOrDefault(n => n.TaiKhoan == acc && n.MatKhau == mk);
                if (ad != null)
                {
                    Session[SesAdmin] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng";
            }
            return View();
        }

        //quản lí giảng viên
        public ActionResult GV()
        {
            return View(db.GIANGVIENs.ToList());
        }

        [HttpGet]
        public ActionResult AddGV()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGV(GIANGVIEN gv)
        {
            if (ModelState.IsValid)
            {
                db.GIANGVIENs.InsertOnSubmit(gv);
                db.SubmitChanges();
                return RedirectToAction("GV");
            }
            return View(gv);
        }

        [HttpGet]
        public ActionResult Xoagv(int id)
        {
            GIANGVIEN gv = db.GIANGVIENs.SingleOrDefault(n => n.MaGV == id);
            ViewBag.MaGV = gv.MaGV;
            if (gv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(gv);
        }

        [HttpPost, ActionName("Xoagv")]
        public ActionResult Xacnhanxoa(int id)
        {
            GIANGVIEN gv = db.GIANGVIENs.SingleOrDefault(n => n.MaGV == id);
            ViewBag.MaGV = gv.MaGV;
            if (gv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.GIANGVIENs.DeleteOnSubmit(gv);
            db.SubmitChanges();
            return RedirectToAction("GV");
        }

        //quản lí khóa học
        public ActionResult KH()
        {
            return View(db.KHOAHOCs.ToList());
        }

        [HttpGet]
        public ActionResult Duyetkh(int id)
        {
            KHOAHOC kh = db.KHOAHOCs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = kh.MaKH;
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duyetkh(KHOAHOC kh)
        {
            if (ModelState.IsValid)
            {
                UpdateModel(kh);
                db.SubmitChanges();
                return RedirectToAction("GV");
            }
            return View(kh);
        }
    }
}