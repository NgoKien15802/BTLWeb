using codeBTL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace codeBTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("SmartPhonesAdmin")]
    [Route("SmartPhonesAdmin/homeadmin")]
    public class SmartPhonesAdminController : Controller
    {
        DtddContext db = new DtddContext();

        [Route("")]
        [Route("index")]



        /// <summary>
        ///     Lấy danh sách điện thoại
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetAllSmartPhones")]
        public IActionResult GetAllSmartPhones(int? page, int? pageSize, string? filter)
        {
            int defaultPageSize = 5;
            if(page != null)
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
                var lstSpFilter = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI01") && x.TenSp.Contains(filter)).ToList();
                PagedList<Sanpham> lstFilter = new PagedList<Sanpham>(lstSpFilter, pageNumber, currentPageSize);
                return View(lstFilter);
            }
            var lstSp = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI01")).ToList();
            PagedList<Sanpham> lst = new PagedList<Sanpham>(lstSp, pageNumber, currentPageSize);
            return View(lst);
        }

        [Route("GetAllSmartPhonesTable")]
        public IActionResult GetAllSmartPhonesTable(int? page, int? pageSize, string? filter)
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
                var lstSpFilter = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI01") && x.TenSp.Contains(filter)).ToList();
                PagedList<Sanpham> lstFilter = new PagedList<Sanpham>(lstSpFilter, pageNumber, currentPageSize);
                return PartialView("GetAllSmartPhonesTable", lstFilter);
            }
            var lstSp = db.Sanphams.AsNoTracking().Where(x => x.MaLoai.Equals("LOAI01")).ToList();
            PagedList<Sanpham> lst = new PagedList<Sanpham>(lstSp, pageNumber, currentPageSize);
            return PartialView("GetAllSmartPhonesTable", lst);
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
            var maxMaSP = db.Sanphams.Where(x => x.MaLoai.Equals("LOAI01")).Max(x => x.MaSp);
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
            sp.AnhDaiDien = sp.formFile.FileName;
             // Lưu tên tệp ảnh vào đối tượng Sanpham
                // Lấy tên tệp ảnh
                db.Sanphams.Add(sp);
                db.SaveChanges();
                return RedirectToAction("AddSmartPhoneDetails", rv);

            return View(sp);
        }



        /// <summary>   
        ///      Create 1 ctsp
        /// </summary>
        /// <returns></returns>
        [Route("AddSmartPhoneDetails")]
        public IActionResult AddSmartPhoneDetails()
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
        [Route("AddSmartPhoneDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSmartPhoneDetails(Chitietsp chitietsp)
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
        ///      Create 1 ct anh
        /// </summary>
        /// <returns></returns>
        [Route("AddImageDetails")]
        public IActionResult AddImageDetails()
        {
            ViewBag.MaSp = new SelectList(db.Sanphams.Where(x => x.MaLoai.Equals("LOAI01")).ToList(), "MaSp", "TenSp");
            return View();
        }

        /// <summary>
        ///     Create 1 sp
        /// </summary>
        [Route("AddImageDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddImageDetails(Chitietanh chitietanh)
        {
            ViewBag.MaSp = new SelectList(db.Sanphams.Where(x => x.MaLoai.Equals("LOAI01")).ToList(), "MaSp", "TenSp");
            if (ModelState.IsValid)
            {
                db.Chitietanhs.Add(chitietanh);
                db.SaveChanges();
                return RedirectToAction("GetAllSmartPhones");
            }
            return View(chitietanh);
        }


        /// <summary>
        ///     Sửa điện thoại
        /// </summary>
        [Route("EditSmartPhones")]
        [HttpGet]
        public IActionResult EditSmartPhones(string maSp)
        {
            ViewBag.MaHangSx = new SelectList(db.Hangs.ToList(), "MaHangSx", "TenHangSx");
            ViewBag.MaLoai = new SelectList(db.Loaisps.Where(x => x.MaLoai.Equals("LOAI01")), "MaLoai", "TenLoai");
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
                RouteValueDictionary rv = new RouteValueDictionary();
                var MaSPNumber = int.Parse(sp.MaSp.Substring(2));
                rv.Add("MaCTSP", "CTSP"+ (MaSPNumber < 10 ? "0" + MaSPNumber : MaSPNumber));
                return RedirectToAction("EditSmartPhoneDetails", rv);
            }
            return View(sp);
        }


        /// <summary>
        ///     Sửa chi tiết điện thoại
        /// </summary>
        [Route("EditSmartPhoneDetails")]
        [HttpGet]
        public IActionResult EditSmartPhoneDetails(string MaCTSP)
        {
            var sp = db.Chitietsps.Find(MaCTSP);
            ViewBag.MaSp = sp.MaSp;
            return View(sp);
        }


        /// <summary>
        ///     Sửa chi điện thoại
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("EditSmartPhoneDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSmartPhoneDetails(Chitietsp ctsp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ctsp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllSmartPhones");
            }
            return View(ctsp);
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
