using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;

namespace codeBTL.Controllers
{
    public class AccessController : Controller
    {
        DtddContext db = new DtddContext();
        // login trước lúc ktra
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
        public IActionResult Login(Taikhoan tk)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {

                var obj = db.Taikhoans.Where(x => x.Username == tk.Username && x.Password == tk.Password).FirstOrDefault();
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
    }
}
