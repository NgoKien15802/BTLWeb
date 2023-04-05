using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace codeBTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("UsersAdmin")]
    [Route("UsersAdmin/homeadmin")]
    public class UsersAdminController : Controller
    {

        DtddContext db = new DtddContext();

        [Route("")]
        [Route("index")]


        /// <summary>
        ///     Lấy danh sách người dùng
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers(int? page, int? pageSize, string? filter)
        {
            int defaultPageSize = 5;
            if (page != null)
            {
                ViewBag.pageSize = pageSize;
            }
            if (filter != null)
            {
                ViewBag.filter = filter;
            }
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
            if (pageSize != null)
            {
                ViewBag.pageSize = pageSize;
            }
            if (filter != null)
            {
                ViewBag.filter = filter;
                var lstUserFilter = db.Userinfos.Where(x => x.Username.Contains(filter)).ToList();
                PagedList<Userinfo> lstFilter = new PagedList<Userinfo>(lstUserFilter, pageNumber, currentPageSize);
                return PartialView("GetAllUsersTable", lstFilter);
            }
            var lstUser = db.Userinfos.AsNoTracking().OrderBy(x => x.Username);
            PagedList<Userinfo> lst = new PagedList<Userinfo>(lstUser, pageNumber, currentPageSize);
            return PartialView("GetAllUsersTable", lst);
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
                return RedirectToAction("GetAllUsers", "UsersAdmin");
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
                return RedirectToAction("GetAllUsers", "UsersAdmin");
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
                return RedirectToAction("GetAllUsers", "UsersAdmin");
            }
            var hoadonbans = db.Hoadonbans.Where(x => x.UserId == maKH).ToList();
            if (hoadonbans.Count() > 0)
            {
                TempData["Message"] = $"Khách hàng có mã {maKH} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllUsers", "UsersAdmin");
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

            return RedirectToAction("GetAllUsers", "UsersAdmin");
        }

    }
}
