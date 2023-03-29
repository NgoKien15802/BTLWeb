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



        [Route("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
           
            return View();
        }

        [Route("GetAllSmartPhones")]
        public IActionResult GetAllSmartPhones()
        {
            return View();
        }


        [Route("GetAllPhoneAccessories")]
        public IActionResult GetAllPhoneAccessories()
        {
            return View();
        }

        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return View();
        }

        [Route("SalesReport")]
        public IActionResult SalesReport()
        {
            return View();
        }
    }
}
