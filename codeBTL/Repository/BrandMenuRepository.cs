using codeBTL.Repository;
using codeBTL.Models;

namespace codeBTL.Repository
{
	public class BrandMenuRepository : IBrandMenuRepository
	{
		private readonly DtddContext _context;
		public BrandMenuRepository(DtddContext context)
		{
			_context = context;
		}

		public Hang Add(Hang hangSx)
		{
			_context.Hangs.Add(hangSx);
			_context.SaveChanges();
			return hangSx;
		}

		public Hang Delete(string maHangSx)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Hang> GetAllHangSx()
		{
			return _context.Hangs;
		}

		public Hang GetHangSx(string maHangSx)
		{
			return _context.Hangs.Find(maHangSx);
		}

		public Hang Update(Hang hangSx)
		{
			_context.Update(hangSx);
			_context.SaveChanges();
			return hangSx;
		}
	}
}
