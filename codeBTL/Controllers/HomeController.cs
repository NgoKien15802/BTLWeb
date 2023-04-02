
using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

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

        public IActionResult Index(string searchString = "", int page = 1)
        {
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            where sp.TenSp.Contains(searchString) // Lọc sản phẩm theo tên
                            orderby sp.TenSp
                            select new ViewModels.SanPhamViewModel
                            {
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).ToList();

            int pageSize = 9;
            var pagedList = new PagedList<ViewModels.SanPhamViewModel>(products, page, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                ViewBag.searchString = searchString;
                return PartialView("_SearchResultIndex", pagedList);
            } else
            {
                return View(pagedList);
            }


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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}