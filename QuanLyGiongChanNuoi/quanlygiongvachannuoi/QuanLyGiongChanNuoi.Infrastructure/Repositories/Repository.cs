using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyGiongChanNuoi.Core.Interfaces;
using QuanLyGiongChanNuoi.Infrastructure.Data;

namespace QuanLyGiongChanNuoi.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly QuanLyGiongChanNuoiContext _context;
		protected readonly DbSet<T> _dbSet;

		public Repository(QuanLyGiongChanNuoiContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.FirstOrDefaultAsync(predicate);
		}

		public virtual async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public virtual async Task AddRangeAsync(IEnumerable<T> entities)
		{
			await _dbSet.AddRangeAsync(entities);
		}

		public virtual void Update(T entity)
		{
			_dbSet.Update(entity);
		}

		public virtual void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public virtual void DeleteRange(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}

		public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
		{
			if (predicate == null)
				return await _dbSet.CountAsync();

			return await _dbSet.CountAsync(predicate);
		}

		public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.AnyAsync(predicate);
		}
	}
}