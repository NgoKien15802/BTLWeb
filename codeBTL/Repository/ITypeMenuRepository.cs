using codeBTL.Models;
namespace codeBTL.Repository
{
	public interface ITypeMenuRepository
	{
		Loaisp Add(Loaisp loaiSp);
        Loaisp Update(Loaisp loaiSp);
        Loaisp Delete(string maLoai);
        Loaisp GetLoaiSp(string maLoai);
		IEnumerable<Loaisp> GetAllLoaiSp();
	}
}
