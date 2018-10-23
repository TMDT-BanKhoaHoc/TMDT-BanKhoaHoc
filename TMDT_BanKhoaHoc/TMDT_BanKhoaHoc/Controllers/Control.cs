using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMDT_BanKhoaHoc.Controllers
{
    public class Control : Controller
    {
        // GET: Control
        protected const string SesHocVien = "HocVien";
        protected const string SesNhanVien = "NhanVien";
        protected const string SesAdmin = "Admin";
        protected Models.QLKhoaHocDataContext db;

        public Control()
        {
            db = new Models.QLKhoaHocDataContext();
        }
    }
}