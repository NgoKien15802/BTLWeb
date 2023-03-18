using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;

namespace codeBTL.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            if (login.username == "123" && login.password == "123")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Thông tin không chính xác";
                return View();
            }
        }
    }
}
