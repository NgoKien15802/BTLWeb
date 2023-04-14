using codeBTL.Models;
namespace codeBTL.Repository
{
	public interface IBrandMenuRepository
	{
		Hang Add(Hang hangSx);
		Hang Update(Hang hangSx);	
		Hang Delete(string maHangSx);
		Hang GetHangSx(string maHangSx);
		IEnumerable<Hang> GetAllHangSx();
	}
}
