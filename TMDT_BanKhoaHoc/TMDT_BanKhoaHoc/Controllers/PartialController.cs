using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMDT_BanKhoaHoc.Controllers
{
    public class PartialController : Control
    {
        public ActionResult ListMonHoc()
        {
            var monhoc = db.MONHOCs.Take(10);

            if(monhoc == null)
            {
                return View();
            }

            return View(monhoc.ToList());
        }

        public ActionResult ListGiangVien()
        {
            var giangviens = db.GIANGVIENs.Take(10);

            if (giangviens == null)
            {
                return View();
            }

            return View(giangviens.ToList());
        }
    }
}