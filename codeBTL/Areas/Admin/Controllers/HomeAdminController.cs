using Microsoft.AspNetCore.Mvc;

namespace codeBTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
 
    {
        //admin
        [Route("")]
        //admin/index
        [Route("index")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
