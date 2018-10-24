using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_BanKhoaHoc.Models;

namespace TMDT_BanKhoaHoc.Controllers
{
    public class HocVienController : Control
    {
        // GET: HocVien
        public ActionResult Index()
        {
            if(Session[SesHocVien] != null)
            {
                HOCVIEN hv = (HOCVIEN)Session[SesHocVien];
                return View(hv);
            }
            return RedirectToAction(controllerName: "NguoiDung", actionName: "DangNhap");
        }
    }
}