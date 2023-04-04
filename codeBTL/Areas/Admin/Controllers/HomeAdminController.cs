using Azure;
using codeBTL.Models;
using codeBTL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace codeBTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        DtddContext db = new DtddContext();

        [Route("")]
        [Route("index")]

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        ///     Lấy ra danh sách đơn hàng
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var ordersWithUsers = db.Dondathangs
                         .AsNoTracking()
                         .OrderBy(x => x.TongTien)
                         .Join(db.Userinfos,
                                o => o.UserId,
                                u => u.UserId,
                                (o, u) => new { Order = o, User = u })
                            .Join(db.Chitietddhs,
                                ou => ou.Order.MaDh,
                                ct => ct.MaDh,
                                (ou, ct) =>new OrderViewModel
                                {
                                    IdOrder = ou.Order.MaDh,
                                    UserName = ou.User.Username,
                                    Email = ou.User.Email,
                                    Address = ou.User.DiaChiUser,
                                    TongTien = ou.Order.TongTien,
                                    SLDat = ct.Sldat,
                                    NgayDatHang = ou.Order.NgayDat
                                }).ToList();
            PagedList<OrderViewModel> lst = new PagedList<OrderViewModel>(ordersWithUsers, pageNumber, pageSize);
            return View(lst);
        }


        /// <summary>
        ///     Lấy danh sách điện thoại
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetAllSmartPhones")]
        public IActionResult GetAllSmartPhones(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSp = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI01")).ToList();
            PagedList<Sanpham> lst = new PagedList<Sanpham>(lstSp, pageNumber, pageSize);
            return View(lst);
        }


        /// <summary>
        ///     Lấy danh sách phụ kiện
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetAllPhoneAccessories")]
        public IActionResult GetAllPhoneAccessories(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSp = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI02")).ToList();
            PagedList<Sanpham> lst = new PagedList<Sanpham>(lstSp, pageNumber, pageSize);
            return View(lst);
        }

        /// <summary>
        ///     Lấy danh sách người dùng
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers(int? page, int? pageSize, string? filter)
        {
            int defaultPageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            int currentPageSize = pageSize == null || pageSize < 1 ? defaultPageSize : pageSize.Value;
            ViewBag.pageSize = pageSize;
            ViewBag.filter = filter;
            if (filter != null)
            {
                var lstUserFilter = db.Userinfos.Where(x => x.Username.Contains(filter)).ToList();
                PagedList<Userinfo> lstFilter = new PagedList<Userinfo>(lstUserFilter, pageNumber, currentPageSize);
                return View(lstFilter);
            }
            var lstUser = db.Userinfos.AsNoTracking().OrderBy(x => x.Username);
            PagedList<Userinfo> lst = new PagedList<Userinfo>(lstUser, pageNumber, currentPageSize);
            return View(lst);
        }


       

        /// <summary>
        ///     trả về cho partial
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Route("GetAllUsersTable")]
        public IActionResult GetAllUsersTable(int? page, int? pageSize, string? filter)
        {
            int defaultPageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            int currentPageSize = pageSize == null || pageSize < 1 ? defaultPageSize : pageSize.Value;
           
            if (filter != null)
            {
                var lstUserFilter = db.Userinfos.Where(x => x.Username.Contains(filter)).ToList();
                PagedList<Userinfo> lstFilter = new PagedList<Userinfo>(lstUserFilter, pageNumber, currentPageSize);
                return PartialView("GetAllUsersTable", lstFilter);
            }
            var lstUser = db.Userinfos.AsNoTracking().OrderBy(x => x.Username);
            PagedList<Userinfo> lst = new PagedList<Userinfo>(lstUser, pageNumber, currentPageSize);
            return PartialView("GetAllUsersTable", lst);
        }

        /// <summary>
        ///     Trang báo cáo doanh thu
        /// </summary>
        /// <returns></returns>
        [Route("SalesReport")]
        public IActionResult SalesReport()
        {
            return View();
        }


        /// <summary>
        ///     thực hiện gọi khi click thêm khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("AddUsers")]
        public IActionResult AddUsers()
        {
            var maxMaKH = db.Userinfos.Max(x => x.UserId);
            var maxMaKHNumber = int.Parse(maxMaKH.Substring(2)) + 1;
            ViewBag.maxMaKHNumber = "KH" + maxMaKHNumber;
            return View();
        }


        /// <summary>
        ///     Create 1 khách hàng
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("AddUsers")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUsers(Userinfo userinfo)
        {
            var maxMaKH = db.Userinfos.Max(x => x.UserId);
            var maxMaKHNumber = int.Parse(maxMaKH.Substring(2)) + 1;
            ViewBag.maxMaKHNumber = "KH" + maxMaKHNumber;
            if (ModelState.IsValid)
            {
                db.Userinfos.Add(userinfo);
                db.SaveChanges();
                return RedirectToAction("GetAllUsers");
            }
            return View(userinfo);
        }


        /// <summary>
        ///     hàm gọi lấy dữ liệu với maKH và truyền data cho bên view
        /// </summary>
        /// <param name="maKH"></param>
        /// <returns></returns>
        [Route("EditUsers")]
        [HttpGet]
        public IActionResult EditUsers(string maKH)
        {
            var user = db.Userinfos.Find(maKH);
            return View(user);
        }


        /// <summary>
        ///     Sửa khách hàng
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("EditUsers")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUsers(Userinfo userinfo)
        {
            if (ModelState.IsValid)
            {
                //đặt trạng thái của đối tượng TDanhMucSp thành Modified và lưu thay đổi bằng phương thức SaveChanges()
                //của đối tượng db (được giả định là đối tượng DbContext). Sau đó, phương thức chuyển hướng người dùng đến trang 					"DanhSachSanPham"
                //để xem danh sách sản phẩm đã được cập nhật.
                db.Entry(userinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllUsers");
            }
            return View(userinfo);
        }


        /// <summary>
        ///     Xóa 1 user
        /// </summary>
        /// <param name="maKH"></param>
        /// <returns></returns>
        [Route("DeleteUsers")]
        [HttpGet]
        public IActionResult DeleteUsers(string maKH)
        {
            TempData["Message"] = "";
            var donDatHangs = db.Dondathangs.Where(x => x.UserId == maKH).ToList();
            if (donDatHangs.Count() > 0)
            {
                TempData["Message"] = $"Khách hàng có mã {maKH} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllUsers", "HomeAdmin");
            }
            var hoadonbans = db.Hoadonbans.Where(x => x.UserId == maKH).ToList();
            if (hoadonbans.Count() > 0)
            {
                TempData["Message"] = $"Khách hàng có mã {maKH} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllUsers", "HomeAdmin");
            }

            try
            {
                db.Remove(db.Userinfos.Find(maKH));
                db.SaveChanges();
                TempData["Message"] = $"Khách hàng có mã {maKH} đã được xóa";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Xóa khách hàng không thành công: " + ex.Message;
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("GetAllUsers", "HomeAdmin");
        }

        /// <summary>
        ///     thực hiện gọi khi click thêm khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("AddSmartPhones")]
        public IActionResult AddSmartPhones()
        {
            ViewBag.MaLoai = new SelectList(db.Loaisps.Where(x => x.MaLoai.Equals("LOAI01")).ToList(), "MaLoai", "TenLoai");
            ViewBag.MaHangSx = new SelectList(db.Hangs.ToList(), "MaHangSx", "TenHangSx");
            var maxMaSP = db.Sanphams.Where(x=> x.MaLoai.Equals("LOAI01")).Max(x => x.MaSp);
            var maxMaSPNumber = int.Parse(maxMaSP.Substring(2)) + 1;
            ViewBag.maxMaSPNumber = "DT" + maxMaSPNumber;
            return View();
        }


        /// <summary>
        ///     Create 1 sp
        /// </summary>
        [Route("AddSmartPhones")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSmartPhones(Sanpham sp)
        {
            ViewBag.MaLoai = new SelectList(db.Loaisps.Where(x => x.MaLoai.Equals("LOAI01")).ToList(), "MaLoai", "TenLoai");
            ViewBag.MaHangSx = new SelectList(db.Hangs.ToList(), "MaHangSx", "TenHangSx");
            var maxMaSP = db.Sanphams.Where(x => x.MaLoai.Equals("LOAI01")).Max(x => x.MaSp);
            var maxMaSPNumber = int.Parse(maxMaSP.Substring(2)) + 1;
            ViewBag.maxMaSPNumber = "DT" + maxMaSPNumber;
            RouteValueDictionary rv = new RouteValueDictionary();
            rv.Add("MaSp", ViewBag.maxMaSPNumber);
            if (ModelState.IsValid)
            {
                db.Sanphams.Add(sp);
                db.SaveChanges();
                return RedirectToAction("addSmartPhoneDetails", rv);
            }
          
            return View(sp);
        }



        /// <summary>
        ///      Create 1 ctsp
        /// </summary>
        /// <returns></returns>
        [Route("addSmartPhoneDetails")]
        public IActionResult addSmartPhoneDetails()
        {
            ViewBag.MaSp = Request.Query["MaSp"];
            var maxCTMaSP = db.Chitietsps.Max(x => x.MaChiTietSp);
            var maxCTMaSPNumber = int.Parse(maxCTMaSP.Substring(4)) + 1;
            ViewBag.CTMaSp = "CTSP"+maxCTMaSPNumber;
            return View();
        }

        /// <summary>
        ///     Create 1 sp
        /// </summary>
        [Route("addSmartPhoneDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addSmartPhoneDetails(Chitietsp chitietsp)
        {
            ViewBag.MaSp = Request.Query["MaSp"];
            var maxCTMaSP = db.Chitietsps.Max(x => x.MaChiTietSp);
            var maxCTMaSPNumber = int.Parse(maxCTMaSP.Substring(4)) + 1;
            ViewBag.CTMaSp = "CTSP" + maxCTMaSPNumber;
            if (ModelState.IsValid)
            {
                db.Chitietsps.Add(chitietsp);
                db.SaveChanges();
                return RedirectToAction("GetAllSmartPhones");
            }
            return View(chitietsp);
        }


        /// <summary>
        ///     Sửa điện thoại
        /// </summary>
        [Route("EditSmartPhones")]
        [HttpGet]
        public IActionResult EditSmartPhones(string maSp)
        {
            ViewBag.MaHangSx = new SelectList(db.Hangs.ToList(), "MaHangSx", "TenHangSx");
            ViewBag.MaLoai = new SelectList(db.Loaisps.Where(x=> x.MaLoai.Equals("LOAI01")), "MaLoai", "TenLoai");
            var sp = db.Sanphams.Find(maSp);
            return View(sp);
        }


        /// <summary>
        ///     Sửa điện thoại
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("EditSmartPhones")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSmartPhones(Sanpham sp)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllSmartPhones");
            }
            return View(sp);
        }


        /// <summary>
        ///     Xóa 1 user
        /// </summary>
        /// <param name="maKH"></param>
        /// <returns></returns>
        [Route("DeleteSmartPhones")]
        [HttpGet]
        public IActionResult DeleteSmartPhones(string maSp)
        {
            TempData["Message"] = "";
            var donDatHangs = db.Chitietddhs.Where(x => x.MaSp == maSp).ToList();
            if (donDatHangs.Count() > 0)
            {
                TempData["Message"] = $"Sản phẩm có mã {maSp} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllSmartPhones");
            }
            var hoadonhaps = db.Chitiethdns.Where(x => x.MaSp == maSp).ToList();
            if (hoadonhaps.Count() > 0)
            {
                TempData["Message"] = $"Sản phẩm có mã {maSp} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllSmartPhones");
            }
            var hoadonbans = db.Chitiethdbs.Where(x => x.MaSp == maSp).ToList();
            if (hoadonbans.Count() > 0)
            {
                TempData["Message"] = $"Sản phẩm có mã {maSp} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllSmartPhones");
            }
            try
            {
                var listAnh = db.Chitietanhs.Where(x => x.MaSp == maSp).ToList();
                if (listAnh.Any()) db.RemoveRange(listAnh);
                var ctsp = db.Chitietsps.Where(x => x.MaSp == maSp).ToList();
                if (ctsp.Any()) db.RemoveRange(ctsp);
                db.Remove(db.Sanphams.Find(maSp));
                db.SaveChanges();
                TempData["Message"] = $"Sản phẩm có mã {maSp} đã được xóa";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Xóa Sản phẩm không thành công: " + ex.Message;
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("GetAllSmartPhones");
        }
    }
}
