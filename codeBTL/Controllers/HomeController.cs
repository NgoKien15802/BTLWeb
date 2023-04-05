using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

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
                            select new ViewModels.SanPhamViewModel
                            {
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = (decimal)cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<ViewModels.SanPhamViewModel> pagedList = new PagedList<ViewModels.SanPhamViewModel>(products, pageNumber, pageSize);
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
                                      select new ViewModels.SanPhamViewModel
                                      {
                                          TenSp = sp.TenSp,
                                          AnhDaiDien = sp.AnhDaiDien,
                                          DonGiaBan = (decimal)cts.DonGiaBan
                                      }).OrderBy(x => x.TenSp);

                PagedList<ViewModels.SanPhamViewModel> pagedListFilter = new PagedList<ViewModels.SanPhamViewModel>(productsFilter, pageNumber, pageSize);
                return PartialView("SearchProducts", pagedListFilter);
            }
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            orderby sp.TenSp
                            select new ViewModels.SanPhamViewModel
                            {
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = (decimal)cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<ViewModels.SanPhamViewModel> pagedList = new PagedList<ViewModels.SanPhamViewModel>(products, pageNumber, pageSize);
            return PartialView("SearchProducts", pagedList);
        }

        public ActionResult GetTop9Products()
        {
            var topProducts = (from ctdh in db.Chitietddhs
                               join sp in db.Sanphams on ctdh.MaSp equals sp.MaSp
                               join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                               orderby ctdh.Sldat descending
                               select new ViewModels.SanPhamViewModel
                               {
                                   TenSp = sp.TenSp,
                                   AnhDaiDien = sp.AnhDaiDien,
                                   DonGiaBan = (decimal)cts.DonGiaBan
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

        public IActionResult Order()
        {
            return View();
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