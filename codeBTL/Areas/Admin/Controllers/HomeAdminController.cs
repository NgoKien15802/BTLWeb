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
        ///     Trang báo cáo doanh thu
        /// </summary>
        /// <returns></returns>
        [Route("SalesReport")]
        public IActionResult SalesReport()
        {
            return View();
        }

    }
}
