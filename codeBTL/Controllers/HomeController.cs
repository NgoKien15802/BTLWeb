using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using codeBTL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace codeBTL.Controllers
{
    public class HomeController : Controller
    {
        DtddContext db = new DtddContext();
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            orderby sp.TenSp
                            select new Models.ViewModels.SanPhamViewModel
                            {
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<SanPhamViewModel> pagedList = new PagedList<SanPhamViewModel>(products, pageNumber, pageSize);
            return View(pagedList);
        }

        public IActionResult SearchProducts(int? page, string? searchString)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if(searchString != null)
            {
                var productsFilter = (from sp in db.Sanphams
                                      join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                                      where sp.TenSp.Contains(searchString) // Lọc sản phẩm theo tên
                                      orderby sp.TenSp
                                      select new Models.ViewModels.SanPhamViewModel
                                      {
                                          TenSp = sp.TenSp,
                                          AnhDaiDien = sp.AnhDaiDien,
                                          DonGiaBan = cts.DonGiaBan
                                      }).OrderBy(x => x.TenSp);

                PagedList<SanPhamViewModel> pagedListFilter = new PagedList<SanPhamViewModel>(productsFilter, pageNumber, pageSize);
                return PartialView("SearchProducts", pagedListFilter);
            }
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            orderby sp.TenSp
                            select new Models.ViewModels.SanPhamViewModel
                            {
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<SanPhamViewModel> pagedList = new PagedList<SanPhamViewModel>(products, pageNumber, pageSize);
            return PartialView("SearchProducts", pagedList);
        }

        public ActionResult GetTop9Products()
        {
            var topProducts = (from ctdh in db.Chitietddhs
                               join sp in db.Sanphams on ctdh.MaSp equals sp.MaSp
                               join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                               orderby ctdh.Sldat descending
                               select new Models.ViewModels.SanPhamViewModel
                               {
                                   TenSp = sp.TenSp,
                                   AnhDaiDien = sp.AnhDaiDien,
                                   DonGiaBan = cts.DonGiaBan
                               }).Take(9).ToList();

            return PartialView("GetTop9Products", topProducts);
        }

        public IActionResult SmartPhone()
        {
            return View();
        }

        public IActionResult PhoneAccessories()
        {

            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }

        public IActionResult SmartphoneDetails()
        {
            return View();
        }

        public IActionResult Order(string maDh)
        {
            // Lấy thông tin đơn đặt hàng và chi tiết đơn đặt hàng tương ứng
            var donDatHang = db.Dondathangs.Include(d => d.User).Where(d => d.MaDh == maDh).FirstOrDefault();
            var chiTietDDHs = db.Chitietddhs.Include(c => c.MaSpNavigation).Where(c => c.MaDh == maDh).ToList();

            // Tạo ViewModel và truyền dữ liệu vào
            var viewModel = new OrderDetailsViewModel();
            viewModel.MaDh = donDatHang.MaDh;
            viewModel.NgayDat = donDatHang.NgayDat;
            viewModel.Username = donDatHang.User.Username;
            viewModel.DiaChiUser = donDatHang.User.DiaChiUser;
            viewModel.TenSp = SanPham.TenSp;
            viewModel.TongTien = chiTietDDHs.Sum(c => c.Sldat * c.DonGiaBan);

            // Trả về View với ViewModel
            return View(viewModel);

        }
     
        public IActionResult CreateOrder(string maSp)
        {
            // Tạo đơn đặt hàng mới
            var donDatHang = new Dondathang();

            // Tạo mã đơn đặt hàng tự động
            int nextMaDh = db.Dondathangs.Count() + 1;
            donDatHang.MaDh = "DDH" + nextMaDh.ToString();

            // Lấy UserId của người đăng nhập hiện tại
            string userId = User.Identity.GetUserId();

            // Cập nhật thông tin cho đơn đặt hàng mới
            donDatHang.UserId = userId;
            donDatHang.NgayDat = DateTime.Now;

            // Thêm đơn đặt hàng vào cơ sở dữ liệu
            db.Dondathangs.Add(donDatHang);
            db.SaveChanges();

            // Tạo chi tiết đơn đặt hàng mới cho sản phẩm được chọn
            var sanPham = db.Sanphams.Find(maSp);
            var donGiaBan = db.Chitietsps.Where(c => c.MaSp == maSp).Select(c => c.DonGiaBan).FirstOrDefault();

            var chiTietDDH = new Chitietddh();
            chiTietDDH.MaDh = donDatHang.MaDh;
            chiTietDDH.MaSp = maSp;
            chiTietDDH.Sldat = 1;

            db.Chitietddhs.Add(chiTietDDH);
            db.SaveChanges();

            // Chuyển hướng sang trang ChiTietDDH với mã đơn đặt hàng
            return RedirectToAction("Order", new { maDh = donDatHang.MaDh });
        }

        public IActionResult HistoryOrder()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}