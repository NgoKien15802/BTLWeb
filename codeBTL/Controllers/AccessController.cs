
using codeBTL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codeBTL.Controllers
{
    public class AccessController : Controller
    {

        DtddContext db = new DtddContext();

        [HttpGet]
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else if (HttpContext.Session.GetString("Username") == "admin")
            {
                return RedirectToAction("index", "admin");
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
            var obj = db.Userinfos.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (obj != null)
            {

                if (obj.Role == 1)
                {
                    HttpContext.Session.SetString("Username", obj.Username.ToString());
                    HttpContext.Session.SetString("UserId", obj.UserId.ToString());
                    return RedirectToAction("index", "admin");
                }
                else
                {
                    HttpContext.Session.SetString("Username", obj.Username.ToString());
                    HttpContext.Session.SetString("UserId", obj.UserId.ToString());
                    return RedirectToAction("Index", "Home");
                }

            }

            // Trả về trang Login với thông báo lỗi tương ứng
            return View();
            //return RedirectToAction("Index", "Home");
        }



        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login", "Access");
        }
        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgortPassword()
        {
            return View();
        }
        /* [AllowAnonymous, HttpGet("forgot-password")]*/
        /*  public IActionResult ForgetPassword(ForgetPassword model)
          {
              if (!ModelState.IsValid)
              {
                  ModelState.Clear();
                  model.EmailSent = true;
              }
              return View();
          }*/

    }
}
