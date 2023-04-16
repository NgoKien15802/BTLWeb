using codeBTL.Repository;
using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;
namespace codeBTL.ViewComponents
{
	public class TypeMenuViewComponent : ViewComponent
	{
		private readonly ITypeMenuRepository _loaiSp;
		public TypeMenuViewComponent (ITypeMenuRepository TypeMenuRepository)
		{
            _loaiSp = TypeMenuRepository;
		}

		public IViewComponentResult Invoke()
		{
			var loaiSp = _loaiSp.GetAllLoaiSp().Where(x => x.MaLoai != "LOAI01").OrderBy(x => x.TenLoai);
			return View(loaiSp);
		}
	}
}
