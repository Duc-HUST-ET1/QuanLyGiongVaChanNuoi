using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using QuanLyGiongChanNuoi.Core.Interfaces;
using QuanLyGiongChanNuoi.Infrastructure.Data;
using QuanLyGiongChanNuoi.Infrastructure; // <--- Thêm dòng này

namespace QuanLyGiongChanNuoi.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuanLyGiongVaThucAnChanNuoiContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(QuanLyGiongVaThucAnChanNuoiContext context)
        {
            _context = context;

            // Khởi tạo repositories
            GiongVatNuoi = new GiongVatNuoiRepository(_context);
            NguoiDung = new NguoiDungRepository(_context);
        }

        public IGiongVatNuoiRepository GiongVatNuoi { get; private set; }
        public ICoSoVatNuoiRepository CoSoVatNuoi { get; private set; }
        public IThucAnChanNuoiRepository ThucAnChanNuoi { get; private set; }
        public ICoSoThucAnRepository CoSoThucAn { get; private set; }
        public INguoiDungRepository NguoiDung { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}