using Azure;
using codeBTL.Models;
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
            return View();
        }


       /* /// <summary>
        ///     Create 1 khách hàng
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("AddSmartPhones")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSmartPhones(Chitietsp sp)
        {
            if (ModelState.IsValid)
            {
                db.Userinfos.Add(userinfo);
                db.SaveChanges();
                return RedirectToAction("GetAllUsers");
            }
            return View(userinfo);
        }
*/
    }
}
