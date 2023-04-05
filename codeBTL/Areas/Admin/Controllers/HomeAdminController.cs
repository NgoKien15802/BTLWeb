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
