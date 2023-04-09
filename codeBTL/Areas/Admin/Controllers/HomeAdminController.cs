using Azure;
using codeBTL.Models;
using codeBTL.Models.Authentication;
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

        [Authentication]

        public IActionResult Index()
        {
            var countUser = db.Userinfos.AsNoTracking().Select(x => x.UserId).Distinct().Count();
            ViewBag.countUser = countUser;
            var countOrder = db.Dondathangs.AsNoTracking().Select(x => x.MaDh).Distinct().Count();
            ViewBag.countOrder = countOrder;
            var countProduct = db.Sanphams.AsNoTracking().Select(x => x.MaSp).Distinct().Count();
            ViewBag.countProduct = countProduct;

            var orders = db.Dondathangs.OrderByDescending(o => o.NgayDat)
                .Take(4)
                .Join(db.Userinfos, o => o.UserId, u => u.UserId, (o, u) => new { Order = o, User = u })
                .ToList();
            var users = db.Userinfos.OrderByDescending(u => u.CreatedAt).Take(4).ToList();


            ViewBag.Orders = orders;
            ViewBag.Users = users;

            return View();
        }


    }
}
