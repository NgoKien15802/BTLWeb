
using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;

namespace codeBTL.Controllers
{
    public class AccessController : Controller
    {
       
        DtddContext db = new DtddContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }

        // login vào hệ thống
        [HttpPost]
        public IActionResult Login(Userinfo user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {

                var obj = db.Userinfos.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (obj != null)
                {
                    // nếu có tồn tại thì set string cho 1 key = username và value là tên                   
                    HttpContext.Session.SetString("UserName", obj.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }

            }
            // nếu ko có tồn tại trong db thì vẫn trang login
            return View();
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
        }
    }
}
