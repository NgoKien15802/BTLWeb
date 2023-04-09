
using codeBTL.Models;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Userinfos.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
                if (obj != null)
                {
                    HttpContext.Session.SetString("UserName", obj.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }

            // Trả về trang Login với thông báo lỗi tương ứng
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View(user);
            //return RedirectToAction("Index", "Home");
        }



        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
        }
        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgortPassword()
        {
            return View();
        }
        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgetPassword(ForgetPassword model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                model.EmailSent = true;
            }
            return View();
        }

    }
}
