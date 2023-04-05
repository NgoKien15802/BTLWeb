using Microsoft.AspNetCore.Mvc;

namespace codeBTL.Areas.Admin.Controllers
{
    public class OrderAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
