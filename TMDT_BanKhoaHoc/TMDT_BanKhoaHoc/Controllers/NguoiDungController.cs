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

        // GET: NguoiDung/Create
        public ActionResult Create()
        {
            if(Session[SesHocVien] != null)
            {
                return Redirect("/");
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(HOCVIEN hocvien)
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
