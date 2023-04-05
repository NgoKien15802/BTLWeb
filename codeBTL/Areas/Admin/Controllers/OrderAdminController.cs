using codeBTL.Models;
using codeBTL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace codeBTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("OrderAdmin")]
    [Route("OrderAdmin/homeadmin")]
    public class OrderAdminController : Controller
    {
        DtddContext db = new DtddContext();

        [Route("")]
        [Route("index")]

        /// <summary>
        ///     Lấy danh sách đơn hàng
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders(int? page, int? pageSize, string? filter)
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
                var lstSpFilter = db.Dondathangs
                         .AsNoTracking()
                         .Where(x => x.User.Username.Contains(filter))
                         .Join(db.Userinfos,
                                o => o.UserId,
                                u => u.UserId,
                                (o, u) => new { Order = o, User = u })
                            .Join(db.Chitietddhs,
                                ou => ou.Order.MaDh,
                                ct => ct.MaDh,
                                (ou, ct) => new OrderViewModel
                                {
                                    IdOrder = ou.Order.MaDh,
                                    UserName = ou.User.Username,
                                    Email = ou.User.Email,
                                    Address = ou.User.DiaChiUser,
                                    TongTien = (decimal)ou.Order.TongTien,
                                    SLDat = (int)ct.Sldat,
                                    NgayDatHang = (DateTime)ou.Order.NgayDat
                                }).ToList();
                PagedList<OrderViewModel> lstFilter = new PagedList<OrderViewModel>(lstSpFilter, pageNumber, currentPageSize);
                return View(lstFilter);
            }
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
                                (ou, ct) => new OrderViewModel
                                {
                                    IdOrder = ou.Order.MaDh,
                                    UserName = ou.User.Username,
                                    Email = ou.User.Email,
                                    Address = ou.User.DiaChiUser,
                                    TongTien = (decimal)ou.Order.TongTien,
                                    SLDat = (int)ct.Sldat,
                                    NgayDatHang = (DateTime)ou.Order.NgayDat
                                }).ToList();
            PagedList<OrderViewModel> lst = new PagedList<OrderViewModel>(ordersWithUsers, pageNumber, currentPageSize);
            return View(lst);
        }

        [Route("GetAllOrdersTable")]
        public IActionResult GetAllOrdersTable(int? page, int? pageSize, string? filter)
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
                var lstSpFilter = db.Dondathangs
                          .AsNoTracking()
                          .Where(x => x.User.Username.Contains(filter))
                          .Join(db.Userinfos,
                                 o => o.UserId,
                                 u => u.UserId,
                                 (o, u) => new { Order = o, User = u })
                             .Join(db.Chitietddhs,
                                 ou => ou.Order.MaDh,
                                 ct => ct.MaDh,
                                 (ou, ct) => new OrderViewModel
                                 {
                                     IdOrder = ou.Order.MaDh,
                                     UserName = ou.User.Username,
                                     Email = ou.User.Email,
                                     Address = ou.User.DiaChiUser,
                                     TongTien = (decimal)ou.Order.TongTien,
                                     SLDat = (int)ct.Sldat,
                                     NgayDatHang = (DateTime)ou.Order.NgayDat
                                 }).ToList();
                PagedList<OrderViewModel> lstFilter = new PagedList<OrderViewModel>(lstSpFilter, pageNumber, currentPageSize);
                return PartialView("GetAllOrdersTable", lstFilter);
            }
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
                                (ou, ct) => new OrderViewModel
                                {
                                    IdOrder = ou.Order.MaDh,
                                    UserName = ou.User.Username,
                                    Email = ou.User.Email,
                                    Address = ou.User.DiaChiUser,
                                    TongTien = (decimal)ou.Order.TongTien,
                                    SLDat = (int)ct.Sldat,
                                    NgayDatHang = (DateTime)ou.Order.NgayDat
                                }).ToList();
            PagedList<OrderViewModel> lst = new PagedList<OrderViewModel>(ordersWithUsers, pageNumber, currentPageSize);
            return PartialView("GetAllOrdersTable", lst);
        }

        /// <summary>
        ///     thực hiện gọi khi click thêm khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("AddOrders")]
        public IActionResult AddOrders()
        {
            ViewBag.MaNv = new SelectList(db.Nhanviens.ToList(), "MaNv", "TenNv");
            ViewBag.UserId = new SelectList(db.Userinfos.ToList(), "UserId", "Username");
            var maxMaDH = db.Dondathangs.Max(x => x.MaDh);
            var maxMaDHNumber = int.Parse(maxMaDH.Substring(3)) + 1;
            ViewBag.maxMaDHNumber = "DDH" + maxMaDHNumber;
            return View();
        }


        /// <summary>
        ///     Create 1 đơn hàng
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("AddOrders")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrders(Dondathang dondathang)
        {
            ViewBag.MaNv = new SelectList(db.Nhanviens.ToList(), "MaNv", "TenNv");
            ViewBag.UserId = new SelectList(db.Userinfos.ToList(), "UserId", "Username");
            var maxMaDH = db.Dondathangs.Max(x => x.MaDh);
            var maxMaDHNumber = int.Parse(maxMaDH.Substring(3)) + 1;
            ViewBag.maxMaDHNumber = "DDH" + maxMaDHNumber;
            RouteValueDictionary rv = new RouteValueDictionary();
            rv.Add("MaDH", ViewBag.maxMaDHNumber);
            if (ModelState.IsValid)
            {
                db.Dondathangs.Add(dondathang);
                db.SaveChanges();
                return RedirectToAction("AddOrdersDetails", rv);
            }
            return View(dondathang);
        }

        /// <summary>   
        ///      Create 1 ct đơn hàng
        /// </summary>
        /// <returns></returns>
        [Route("AddOrdersDetails")]
        public IActionResult AddOrdersDetails()
        {
            ViewBag.MaDH = Request.Query["MaDH"];
            ViewBag.MaSp = new SelectList(db.Sanphams.ToList(), "MaSp", "TenSp");
            return View();
        }

        /// <summary>
        ///     Create 1 chi ddh
        /// </summary>
        [Route("AddOrdersDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrdersDetails(Chitietddh chitietddh)
        {
            ViewBag.MaDH = Request.Query["MaDH"];
            ViewBag.MaSp = new SelectList(db.Sanphams.ToList(), "MaSp", "TenSp");
            if (ModelState.IsValid)
            {
                db.Chitietddhs.Add(chitietddh);
                db.SaveChanges();
                return RedirectToAction("GetAllOrders");
            }
            return View(chitietddh);
        }

        /// <summary>
        ///     Sửa đơn hàng
        /// </summary>
        [Route("EditOrders")]
        [HttpGet]
        public IActionResult EditOrders(string maDH)
        {
            ViewBag.MaNv = new SelectList(db.Nhanviens.ToList(), "MaNv", "TenNv");
            ViewBag.UserId = new SelectList(db.Userinfos.ToList(), "UserId", "Username");
            var dh = db.Dondathangs.Find(maDH);
            return View(dh);
        }


        /// <summary>
        ///     Sửa chi tiết hóa đơn
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("EditOrders")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrders(Dondathang dondathang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dondathang).State = EntityState.Modified;
                db.SaveChanges();
                RouteValueDictionary rv = new RouteValueDictionary();
                rv.Add("MaDH", dondathang.MaDh);
                return RedirectToAction("EditOrderDetails", rv);
            }
            return View(dondathang);
        }

        /// <summary>
        ///     Sửa đơn hàng
        /// </summary>
        [Route("EditOrderDetails")]
        [HttpGet]
        public IActionResult EditOrderDetails(string maDH)
        {
            ViewBag.MaSp = new SelectList(db.Sanphams.ToList(), "MaSp", "TenSp");
            var dh = db.Chitietddhs.Find(maDH);
            return View(dh);
        }


        /// <summary>
        ///     Sửa chi tiết hóa đơn
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("EditOrderDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrderDetails(Chitietddh chitietddh)
        {
            if (ModelState.IsValid)
            {
                try
                {// Select the record that you want to update
                    var originalChitietddh = db.Chitietddhs.Find(chitietddh.MaDh);

                    // Modify the selected record
                    originalChitietddh.MaSp = chitietddh.MaSp;
                    originalChitietddh.KhuyenMai = chitietddh.KhuyenMai;
                    originalChitietddh.Sldat = chitietddh.Sldat;

                    // Save the changes back to the database
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    return View("Error", ex);
                }

                return RedirectToAction("GetAllOrders");
            }

            return View(chitietddh);

        }

        /// <summary>
        ///     Xóa 1 hóa đơn
        /// </summary>
        /// <param name="maKH"></param>
        /// <returns></returns>
        [Route("DeleteOrders")]
        [HttpGet]
        public IActionResult DeleteOrders(string maDH)
        {
           
            try
            {
                var ctHD = db.Chitietddhs.Where(x => x.MaDh == maDH).ToList();
                if (ctHD.Any()) db.RemoveRange(ctHD);
                db.Remove(db.Dondathangs.Find(maDH));
                db.SaveChanges();
                TempData["Message"] = $"Đơn đặt hàng có mã {maDH} đã được xóa";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Xóa đơn hàng không thành công: " + ex.Message;
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("GetAllOrders");
        }
    }
}
