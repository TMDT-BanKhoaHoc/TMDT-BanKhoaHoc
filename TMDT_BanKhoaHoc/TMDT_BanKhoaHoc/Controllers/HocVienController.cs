using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_BanKhoaHoc.Models;
using TMDT_BanKhoaHoc.Common;

namespace TMDT_BanKhoaHoc.Controllers
{
    public class HocVienController : Control
    {
        // GET: HocVien
        private Payment payment;

        public ActionResult Index()
        {
            if (Session[SesHocVien] != null)
            {
                HOCVIEN hv = (HOCVIEN)Session[SesHocVien];
                return View(hv);
            }
            return RedirectToAction(controllerName: "NguoiDung", actionName: "DangNhap");
        }

        public ActionResult DangNhap()
        {
            if (Session[SesHocVien] == null)
            {
                return View();
            }
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(FormDangNhap form)
        {
            if (ModelState.IsValid)
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
                Session[SesHocVien] = hv;
                return Redirect("/");
            }
            return View(form);
        }

        public ActionResult DangXuat()
        {
            if (Session[SesHocVien] != null)
            {
                Session[SesHocVien] = null;
            }
            return Redirect("/");
        }

        // GET: NguoiDung/Create
        public ActionResult DangKy()
        {
            if (Session[SesHocVien] != null)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(HOCVIEN hocvien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session[SesHocVien] != null)
                    {
                        return Redirect("/");
                    }

                    HOCVIEN hv = db.HOCVIENs.SingleOrDefault(n => n.Taikhoan == hocvien.Taikhoan);

                    if (hv != null)
                    {
                        ViewData["taikhoan"] = "Tài khoản đã được sử dụng";
                        return View(hocvien);
                    }

                    if (hocvien.Email.IndexOf('@') > 0)
                    {
                        string[] a = hocvien.Email.Split('@');
                        if (a[1].IndexOf('.') < 1)
                        {
                            ViewData["email"] = "Email không hợp lệ";
                            return View(hocvien);
                        }
                    }
                    else
                    {
                        ViewData["email"] = "Email không hợp lệ";
                        return View(hocvien);
                    }

                    db.HOCVIENs.InsertOnSubmit(hocvien);
                    db.SubmitChanges();

                    return RedirectToAction(controllerName: "NguoiDung", actionName: "DangNhap");
                }
                else
                {
                    return View(hocvien);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SuaThongTin()
        {
            if (Session[SesHocVien] == null)
            {
                return RedirectToAction("DangNhap");
            }

            HOCVIEN hv = (HOCVIEN)Session[SesHocVien];

            return View(hv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaThongTin(HOCVIEN hv)
        {
            if (Session[SesHocVien] == null)
            {
                return RedirectToAction("DangNhap");
            }

            if (ModelState.IsValidField("HoTen") && ModelState.IsValidField("Email"))
            {
                HOCVIEN hocvien = (HOCVIEN)Session[SesHocVien];
                HOCVIEN hocVien = db.HOCVIENs.SingleOrDefault(n => n.MaHV == hocvien.MaHV);

                hocVien.HoTen = hv.HoTen;
                if (hv.Ngaysinh != null)
                {
                    hocVien.Ngaysinh = hv.Ngaysinh;
                }

                hocVien.DiachiHV = hv.DiachiHV;
                hocVien.DienthoaiHV = hv.DienthoaiHV;
                hocVien.Email = hv.Email;

                db.SubmitChanges();

                Session[SesHocVien] = hocVien;
                return RedirectToAction(actionName: "Index");
            }
            else
            {
                return View(hv);
            }
        }

        public ActionResult DoiMatKhau()
        {
            if (Session[SesHocVien] == null)
            {
                return RedirectToAction(actionName: "DangNhap");
            }
            ViewData["matkhaucu"] = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(FormDoiMatKhau form)
        {
            if (Session[SesHocVien] == null)
            {
                return RedirectToAction(actionName: "DangNhap");
            }
            ViewData["matkhaucu"] = "";
            if (ModelState.IsValid)
            {
                HOCVIEN hv = db.HOCVIENs.SingleOrDefault(n => n.Matkhau == form.MatKhauCu);
                if (hv == null)
                {
                    ViewData["matkhaucu"] = "Mật khẩu cũ không đúng";
                    return View(form);
                }

                hv.Matkhau = form.MatKhauMoi;
                db.SubmitChanges();
                return RedirectToAction(actionName: "Index");
            }
            else
            {
                form.MatKhauMoi = "";
                return View(form);
            }
        }

        public ActionResult XemGioHang()
        {
            if (Session[SesHocVien] != null)
            {
                List<KHOAHOC> giohang = (List<KHOAHOC>)Session["GioHang"];

                if (giohang == null)
                {
                    return View();
                }

                return View(giohang);
            }
            return RedirectToAction("DangNhap");
        }

        [HttpPost]
        public int XoaGioHang([System.Web.Http.FromBody]string makhoahoc)
        {
            if (Session[SesHocVien] == null)
            {
                return -2;
            }

            int khoahoc;

            if (!int.TryParse(makhoahoc, out khoahoc))
            {
                return -1;
            }

            List<KHOAHOC> giohang = (List<KHOAHOC>)Session["GioHang"];

            KHOAHOC kHOAHOC = giohang.SingleOrDefault(n => n.MaKH == khoahoc);

            if (kHOAHOC == null)
            {
                return -1;
            }

            giohang.Remove(kHOAHOC);

            return giohang.Count;
        }

        public int ThemGioHang(string khoahoc)
        {
            if (Session[SesHocVien] == null)
            {
                return -4;
            }

            HOCVIEN hocvien = (HOCVIEN)Session[SesHocVien];

            int makhoahoc;
            if (!int.TryParse(khoahoc, out makhoahoc))
            {
                return -3;
            }

            KHOAHOC khoaHoc = db.KHOAHOCs.SingleOrDefault(n => n.MaKH == makhoahoc);

            if (khoaHoc == null)
            {
                return -3;
            }

            CHITIETDONTHANG chitiet = db.CHITIETDONTHANGs.SingleOrDefault(n => n.DONDATHANG.HOCVIEN.MaHV == hocvien.MaHV &&
                                        n.KHOAHOC.MaKH == makhoahoc);

            if (chitiet != null)
            {
                return -2;
            }

            if (Session["GioHang"] == null)
            {
                Session["GioHang"] = new List<KHOAHOC>();
            }

            List<KHOAHOC> giohang = (List<KHOAHOC>)Session["GioHang"];

            if (giohang.SingleOrDefault(n => n.MaKH == makhoahoc) != null)
            {
                return -1;
            }

            giohang.Add(khoaHoc);

            return giohang.Count;
        }

        public ActionResult KetThucThanhToan(bool ketqua)
        {
            return View(ketqua);
        }

        public ActionResult ThanhToan()
        {

            HOCVIEN hocvien = (HOCVIEN)Session[SesHocVien];
            List<KHOAHOC> giohang = (List<KHOAHOC>)Session["GioHang"];
            DONDATHANG donhang = new DONDATHANG();
            if (hocvien == null)
            {
                return RedirectToAction(actionName: "DangNhap");
            }

            if(giohang != null)
            {
                
                donhang.MaHV = hocvien.MaHV;
                donhang.Ngaydat = DateTime.Now;
                donhang.Dathanhtoan = false;
                foreach(KHOAHOC khoahoc in giohang)
                {
                    donhang.CHITIETDONTHANGs.Add(new CHITIETDONTHANG() {
                        MaKH = khoahoc.MaKH
                    });
                }
                db.DONDATHANGs.InsertOnSubmit(donhang);
                db.SubmitChanges();
            } else
            {
                return View();
            }

            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/HocVien/ThanhToan?";

                    var guid = Convert.ToString((new Random()).Next(100000));

                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Faile");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error " + ex.Message);
                return View("Faile");
            }

            
            donhang.Dathanhtoan = true;
            db.SubmitChanges();
            return View("Success");
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            float tong = 0;
            List<KHOAHOC> giohang = (List<KHOAHOC>)Session["GioHang"];
            var itemList = new ItemList() { items = new List<Item>() };

            foreach(KHOAHOC khoahoc in giohang)
            {
                float tien = float.Parse(khoahoc.HocPhi.Value.ToString()) / 22700;
                tong += tien;
                itemList.items.Add(new Item()
                {
                    name = khoahoc.MaKH + " - " + khoahoc.TenKH,
                    currency = "USD",
                    price = "5",
                    quantity = "1",
                    sku = khoahoc.MaKH + " - " + khoahoc.TenKH
                });
            }
            float tax = tong / 5;
            var payer = new Payer() { payment_method = "paypal" };
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var detail = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "5"
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = "7",
                details = detail
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction decription",
                invoice_number = "your invoice number",
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return this.payment.Create(apiContext);
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        public ActionResult TheoDoiBienLai()
        {
            if(Session[SesHocVien] != null)
            {
                HOCVIEN hocvien = (HOCVIEN)Session[SesHocVien];
                var bienlai = db.DONDATHANGs.Where(n => n.MaHV == hocvien.MaHV);

                if(bienlai.Count() != 0)
                {
                    return View(bienlai);
                }
                return View();
            }
            return RedirectToAction(actionName: "DangNhap");
        }

        public ActionResult KhoaHocTheoBienLai(int bienlai)
        {
            DONDATHANG Bienlai = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == bienlai);
            if(Bienlai == null)
            {
                return View();
            }

            var chitiets = Bienlai.CHITIETDONTHANGs;

            if(chitiets != null)
            {
                return View(chitiets.ToList());
            }
            return View();
        }
    }
}