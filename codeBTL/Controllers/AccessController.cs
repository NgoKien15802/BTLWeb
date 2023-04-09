
using codeBTL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
                if (HttpContext.Session.GetString("Role") == "1" )
                {
                    return View("Index", "HomeAdmin");
                }
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
                    HttpContext.Session.SetString("Role", obj.Role.ToString());
                    if (obj.Role == 1)
                    {
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                    return RedirectToAction("Index", "Home");
                }
            // Trả về trang Login với thông báo lỗi tương ứng
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View(user);
            //return RedirectToAction("Index", "Home");
            }   
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
        }
    }
}
