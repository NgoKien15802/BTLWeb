using codeBTL.Models;
using codeBTL.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace codeBTL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryOrderAPIController : ControllerBase
    {
        DtddContext db = new DtddContext();
        [HttpGet]
        public IEnumerable<HistoryOrderAPI> GetHistoryOrder()
        {
            string username = HttpContext.Session.GetString("Username");
            var user = db.Userinfos.AsNoTracking().FirstOrDefault(x => x.Username == username);
            var orders = (from ddh in db.Dondathangs
                          join userIn in db.Userinfos on ddh.UserId equals user.UserId
                          join ctdh in db.Chitietddhs on ddh.MaDh equals ctdh.MaDh
                          join sp in db.Sanphams on ctdh.MaSp equals sp.MaSp
                          join ctsp in db.Chitietsps on sp.MaSp equals ctsp.MaSp
                          where ddh.UserId == user.UserId
                          select new HistoryOrderAPI
                          {
                              MaDh = ddh.MaDh,
                              NgayDat = ddh.NgayDat,
                              TongTien = ddh.TongTien,
                              TenSp = sp.TenSp,
                              Sldat = ctdh.Sldat
                          }).ToList();
            return orders;
        }

    }
}
