using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace codeBTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("AccessoriesAdmin")]
    [Route("AccessoriesAdmin/homeadmin")]
    public class AccessoriesAdminController : Controller
    {
        DtddContext db = new DtddContext();

        [Route("")]
        [Route("index")]


        [Route("GetAllPhoneAccessories")]
        public IActionResult GetAllPhoneAccessories(int? page, int? pageSize, string? filter)
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
            if (filter != null)
            {
                var lstSpFilter = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI02") && x.TenSp.Contains(filter)).ToList();
                PagedList<Sanpham> lstFilter = new PagedList<Sanpham>(lstSpFilter, pageNumber, currentPageSize);
                return View(lstFilter);
            }
            var lstSp = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI02")).ToList();
            PagedList<Sanpham> lst = new PagedList<Sanpham>(lstSp, pageNumber, currentPageSize);
            return View(lst);
        }


        [Route("GetAllPhoneAccessoriesTable")]
        public IActionResult GetAllPhoneAccessoriesTable(int? page, int? pageSize, string? filter)
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
                var lstSpFilter = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI02") && x.TenSp.Contains(filter)).ToList();
                PagedList<Sanpham> lstFilter = new PagedList<Sanpham>(lstSpFilter, pageNumber, currentPageSize);
                return PartialView("GetAllPhoneAccessoriesTable", lstFilter);
            }
            var lstSp = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI02")).ToList();
            PagedList<Sanpham> lst = new PagedList<Sanpham>(lstSp, pageNumber, currentPageSize);
            return PartialView("GetAllPhoneAccessoriesTable", lst);
        }




        /// <summary>
        ///     thực hiện gọi khi click thêm khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("AddPhoneAccessories")]
        public IActionResult AddPhoneAccessories()
        {
            ViewBag.MaLoai = new SelectList(db.Loaisps.Where(x => x.MaLoai.Equals("LOAI02")).ToList(), "MaLoai", "TenLoai");
            ViewBag.MaHangSx = new SelectList(db.Hangs.ToList(), "MaHangSx", "TenHangSx");
            var maxMaSP = db.Sanphams.Where(x => x.MaLoai.Equals("LOAI02")).Max(x => x.MaSp);
            var maxMaSPNumber = int.Parse(maxMaSP.Substring(2)) + 1;
            ViewBag.maxMaSPNumber = "PK" + maxMaSPNumber;
            return View();
        }


        /// <summary>
        ///     Create 1 sp
        /// </summary>
        [Route("AddPhoneAccessories")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhoneAccessories(Sanpham sp)
        {
            ViewBag.MaLoai = new SelectList(db.Loaisps.Where(x => x.MaLoai.Equals("LOAI02")).ToList(), "MaLoai", "TenLoai");
            ViewBag.MaHangSx = new SelectList(db.Hangs.ToList(), "MaHangSx", "TenHangSx");
            var maxMaSP = db.Sanphams.Where(x => x.MaLoai.Equals("LOAI02")).Max(x => x.MaSp);
            var maxMaSPNumber = int.Parse(maxMaSP.Substring(2)) + 1;
            ViewBag.maxMaSPNumber = "PK" + maxMaSPNumber;
            RouteValueDictionary rv = new RouteValueDictionary();
            rv.Add("MaSp", ViewBag.maxMaSPNumber);
            if (ModelState.IsValid)
            {
                db.Sanphams.Add(sp);
                db.SaveChanges();
                return RedirectToAction("AddPhoneAccessoriesDetail", rv);
            }

            return View(sp);
        }


        /// <summary>   
        ///      Create 1 ctsp
        /// </summary>
        /// <returns></returns>
        [Route("AddPhoneAccessoriesDetail")]
        public IActionResult AddPhoneAccessoriesDetail()
        {
            ViewBag.MaSp = Request.Query["MaSp"];
            var maxCTMaSP = db.Chitietsps.Max(x => x.MaChiTietSp);
            var maxCTMaSPNumber = int.Parse(maxCTMaSP.Substring(4)) + 1;
            ViewBag.CTMaSp = "CTSP" + maxCTMaSPNumber;
            return View();
        }

        /// <summary>
        ///     Create 1 sp
        /// </summary>
        [Route("AddPhoneAccessoriesDetail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhoneAccessoriesDetail(Chitietsp chitietsp)
        {
            ViewBag.MaSp = Request.Query["MaSp"];
            var maxCTMaSP = db.Chitietsps.Max(x => x.MaChiTietSp);
            var maxCTMaSPNumber = int.Parse(maxCTMaSP.Substring(4)) + 1;
            ViewBag.CTMaSp = "CTSP" + maxCTMaSPNumber;
            if (ModelState.IsValid)
            {
                db.Chitietsps.Add(chitietsp);
                db.SaveChanges();
                return RedirectToAction("GetAllPhoneAccessories");
            }
            return View(chitietsp);
        }



        /// <summary>
        ///     Sửa điện thoại
        /// </summary>
        [Route("EditPhoneAccessorises")]
        [HttpGet]
        public IActionResult EditPhoneAccessorises(string maSp)
        {
            ViewBag.MaHangSx = new SelectList(db.Hangs.ToList(), "MaHangSx", "TenHangSx");
            ViewBag.MaLoai = new SelectList(db.Loaisps.Where(x => x.MaLoai.Equals("LOAI02")), "MaLoai", "TenLoai");
            var sp = db.Sanphams.Find(maSp);
            return View(sp);
        }


        /// <summary>
        ///     Sửa phụ kiện
        /// </summary>
        [Route("EditPhoneAccessorises")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPhoneAccessorises(Sanpham sp)
        {
            if (ModelState.IsValid)
            {

                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllPhoneAccessories");
            }
            return View(sp);
        }


        /// <summary>
        ///     Xóa 1 user
        /// </summary>
        /// <param name="maKH"></param>
        /// <returns></returns>
        [Route("DeletePhoneAccessorises")]
        [HttpGet]
        public IActionResult DeletePhoneAccessorises(string maSp)
        {
            TempData["Message"] = "";
            var donDatHangs = db.Chitietddhs.Where(x => x.MaSp == maSp).ToList();
            if (donDatHangs.Count() > 0)
            {
                TempData["Message"] = $"Sản phẩm có mã {maSp} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllPhoneAccessories");
            }
            var hoadonhaps = db.Chitiethdns.Where(x => x.MaSp == maSp).ToList();
            if (hoadonhaps.Count() > 0)
            {
                TempData["Message"] = $"Sản phẩm có mã {maSp} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllPhoneAccessories");
            }
            var hoadonbans = db.Chitiethdbs.Where(x => x.MaSp == maSp).ToList();
            if (hoadonbans.Count() > 0)
            {
                TempData["Message"] = $"Sản phẩm có mã {maSp} không được xóa";
                TempData["MessageType"] = "error";
                return RedirectToAction("GetAllPhoneAccessories");
            }
            try
            {
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

            return RedirectToAction("GetAllPhoneAccessories");
        }
    }
}
