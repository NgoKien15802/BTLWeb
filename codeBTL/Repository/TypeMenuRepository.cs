using codeBTL.Repository;
using codeBTL.Models;

namespace codeBTL.Repository
{
	public class TypeMenuRepository : ITypeMenuRepository
	{
		private readonly DtddContext _context;
		public TypeMenuRepository(DtddContext context)
		{
			_context = context;
		}

		public Loaisp Add(Loaisp loaiSp)
		{
			_context.Loaisps.Add(loaiSp);
			_context.SaveChanges();
			return loaiSp;
		}

		public Loaisp Delete(string maLoai)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Loaisp> GetAllLoaiSp()
		{
			return _context.Loaisps;
		}

		public Loaisp GetLoaiSp(string maLoai)
		{
			return _context.Loaisps.Find(maLoai);
		}

		public Loaisp Update(Loaisp loaiSp)
		{
			_context.Update(loaiSp);
			_context.SaveChanges();
			return loaiSp;
		}
	}
}
