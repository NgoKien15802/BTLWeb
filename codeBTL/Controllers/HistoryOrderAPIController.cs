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
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var orders = (from ddh in db.Dondathangs
                              join userIn in db.Userinfos on ddh.UserId equals userIn.UserId
                              join ctdh in db.Chitietddhs on ddh.MaDh equals ctdh.MaDh
                              join sp in db.Sanphams on ctdh.MaSp equals sp.MaSp
                              join ctsp in db.Chitietsps on sp.MaSp equals ctsp.MaSp
                              where ddh.UserId == userId
                              select new HistoryOrderAPI
                              {
                                  MaDh = ddh.MaDh,
                                  NgayDat = ddh.NgayDat,
                                  TongTien = ddh.TongTien,
                                  AnhDaiDien = sp.AnhDaiDien,
                                  MaSp = sp.MaSp,
                                  TenSp = sp.TenSp,
                                  Sldat = ctdh.Sldat
                              }).ToList();
                return orders;
            }
            else
            {
                return (IEnumerable<HistoryOrderAPI>)NotFound();
            }
            
        }
        [HttpPut("{MaDh}")]
        public IActionResult UpdateRecord([FromBody] int slDat, [FromRoute] string MaDh, [FromQuery] string MaSp)
        {
            try
            {
                var query = $"UPDATE CHITIETDDH SET SLDat = {slDat} WHERE MaDH = N'{MaDh}' AND MaSP = N'{MaSp}'";
                var result = db.Database.ExecuteSqlRaw(query);

                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
