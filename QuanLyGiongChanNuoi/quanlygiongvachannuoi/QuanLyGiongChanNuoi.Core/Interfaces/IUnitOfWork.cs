using System;
using System.Threading.Tasks;

namespace QuanLyGiongChanNuoi.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		// Repository properties sẽ được thêm vào sau
		IGiongVatNuoiRepository GiongVatNuoi { get; }
		ICoSoVatNuoiRepository CoSoVatNuoi { get; }
		IThucAnChanNuoiRepository ThucAnChanNuoi { get; }
		ICoSoThucAnRepository CoSoThucAn { get; }
		INguoiDungRepository NguoiDung { get; }

		// Lưu thay đổi
		Task<int> SaveChangesAsync();

		// Bắt đầu transaction
		Task BeginTransactionAsync();

		// Commit transaction
		Task CommitTransactionAsync();

		// Rollback transaction
		Task RollbackTransactionAsync();
	}
}