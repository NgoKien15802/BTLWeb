using Microsoft.AspNetCore.Mvc;

namespace codeBTL.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
