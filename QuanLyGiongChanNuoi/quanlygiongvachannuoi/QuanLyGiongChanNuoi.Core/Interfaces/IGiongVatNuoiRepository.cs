using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyGiongChanNuoi.Core.Interfaces
{
    public interface IGiongVatNuoiRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object> GetByIdAsync(int id);
        Task AddAsync(object entity);
        Task UpdateAsync(object entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<object>> GetByLoaiAsync(string loai);
        Task<IEnumerable<object>> SearchByTenGiongAsync(string tenGiong);
        Task<bool> IsTenGiongExistsAsync(string tenGiong, int? excludeId = null);
        Task<IEnumerable<object>> GetAllWithDetailsAsync();
    }

    public interface ICoSoVatNuoiRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object> GetByIdAsync(int id);
        Task AddAsync(object entity);
        Task UpdateAsync(object entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<object>> GetByGiongIdAsync(int giongId);
        Task<IEnumerable<object>> GetByLoaiCoSoAsync(string loaiCoSo);
    }

    public interface IThucAnChanNuoiRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object> GetByIdAsync(int id);
        Task AddAsync(object entity);
        Task UpdateAsync(object entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<object>> GetByLoaiThucAnAsync(string loaiThucAn);
    }

    public interface ICoSoThucAnRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object> GetByIdAsync(int id);
        Task AddAsync(object entity);
        Task UpdateAsync(object entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<object>> GetByThucAnIdAsync(int thucAnId);
        Task<IEnumerable<object>> GetByLoaiCoSoAsync(string loaiCoSo);
    }

    public interface INguoiDungRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object> GetByIdAsync(int id);
        Task AddAsync(object entity);
        Task UpdateAsync(object entity);
        Task DeleteAsync(int id);
        Task<object> GetByTenDangNhapAsync(string tenDangNhap);
        Task<object> AuthenticateAsync(string tenDangNhap, string matKhau);
        Task<bool> IsTenDangNhapExistsAsync(string tenDangNhap, int? excludeId = null);
    }
}