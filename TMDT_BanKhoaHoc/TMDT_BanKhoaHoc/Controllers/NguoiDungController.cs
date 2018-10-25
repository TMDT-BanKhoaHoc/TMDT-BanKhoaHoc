using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TMDT_BanKhoaHoc.Models;

namespace TMDT_BanKhoaHoc.Controllers
{
    public class NguoiDungController : Control
    {

        // GET: NguoiDung/Details/5
        public ActionResult Index()
        {
            var khoahocs = db.KHOAHOCs.Where(n => n.KetQuaDuyet == true);

            if(khoahocs.Count() == 0)
            {
                return View();
            }

            return View(khoahocs);
        }

        public ActionResult ThongKhoaHoc(int khoahoc)
        {
            KHOAHOC khoaHoc = db.KHOAHOCs.SingleOrDefault(n => n.MaKH == khoahoc);

            ViewBag.MuaHang = false;

            if (khoaHoc == null)
            {
                return View();
            }

            if(Session[SesHocVien] == null)
            {
                return View(khoaHoc);
            }

            HOCVIEN hocvien = (HOCVIEN)Session[SesHocVien];

            if (hocvien != null)
            {
                CHITIETDONTHANG chitiet = db.CHITIETDONTHANGs.SingleOrDefault(n => n.DONDATHANG.HOCVIEN.MaHV == hocvien.MaHV
                                            && n.KHOAHOC.MaKH == khoaHoc.MaKH);

                if (chitiet != null)
                {
                    ViewBag.MuaHang = true;
                }
            }

            var baigiangs = khoaHoc.BaiGiangs;
            if (baigiangs.Count != 0)
            {
                ViewBag.BaiGiangs = baigiangs.ToList();
            }
            
            return View(khoaHoc);
        }

        public ActionResult MonHoc(string monhoc)
        {
            int mamonhoc;

            if(!int.TryParse(monhoc, out mamonhoc))
            {
                return View();
            }

            var khoahocs = db.KHOAHOCs.Where(n => n.MONHOC.MaMH == mamonhoc && n.KetQuaDuyet == true);

            if(khoahocs.Count() == 0)
            {
                return View();
            }
            return View(khoahocs.ToList());
        }

        public ActionResult TimKiem(string text)
        {
            var khoahocs = db.KHOAHOCs.Where(n => n.TenKH.Contains(text) && n.KetQuaDuyet == true);
            var giangviens = db.GIANGVIENs.Where(n => n.TenGV.Contains(text));
            var monhocs = db.MONHOCs.Where(n => n.TenMH.Contains(text));
            List<object> list = new List<object>();
            if(khoahocs.Count() > 0)
            {
                foreach(var khoahoc in khoahocs)
                {
                    list.Add(khoahoc);
                }
            }
            if(giangviens.Count() > 0)
            {
                foreach(var giangvien in giangviens)
                {
                    list.Add(giangvien);
                }
            }
            if(monhocs.Count() > 0)
            {
                foreach(var monhoc in monhocs)
                {
                    list.Add(monhoc);
                }
            }

            if(list.Count != 0)
            {
                return View(list);
            }
            return View();
        }

        public ActionResult KhoaHocTheoGiangVien(int giangvien)
        {
            GIANGVIEN giangVien = db.GIANGVIENs.SingleOrDefault(n => n.MaGV == giangvien);

            if(giangVien != null)
            {
                return View(giangVien);
            }

            return View();
        }

        public ActionResult LietKeKhoaHocTheoGiangVien(int giangvien)
        {
            var khoahocs = db.KHOAHOCs.Where(n => n.MaGV == giangvien && n.KetQuaDuyet == true);

            if(khoahocs.Count() > 0)
            {
                return View(khoahocs.ToList());
            }
            return View();
        }

        public ActionResult KhoaHocTheoMonHoc(int monhoc)
        {
            var khoahocs = db.KHOAHOCs.Where(n => n.MaMH == monhoc && n.KetQuaDuyet == true);

            if(khoahocs.Count() > 0)
            {
                ViewBag.Title = khoahocs.First().MONHOC.TenMH;
                return View(khoahocs.ToList());
            }
            return View();
        }
    }
}
