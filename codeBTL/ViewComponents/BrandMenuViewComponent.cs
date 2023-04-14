using codeBTL.Repository;
using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;
namespace codeBTL.ViewComponents
{
	public class BrandMenuViewComponent : ViewComponent
	{
		private readonly IBrandMenuRepository _hangSx;
		public BrandMenuViewComponent (IBrandMenuRepository brandMenuRepository)
		{
			_hangSx = brandMenuRepository;
		}

		public IViewComponentResult Invoke()
		{
			var hangSx = _hangSx.GetAllHangSx().OrderBy(x => x.TenHangSx);
			return View(hangSx);
		}
	}
}
