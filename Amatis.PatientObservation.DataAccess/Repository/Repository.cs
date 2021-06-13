using Amatis.PatientObservation.Common.Entities;
using Amatis.PatientObservation.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amatis.PatientObservation.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async virtual Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
        }
        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }
        public async virtual Task<IEnumerable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>()
                   .Where(x => !x.IsDeleted)
                   .Where(predicate)
                   .ToListAsync();
        }
        public async virtual Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async virtual Task<T> DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            await UpdateAsync(entity);
            return entity;
        }

        public async virtual Task<IEnumerable<T>> GetPagedListAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            return await _dbContext.Set<T>()
                .Where(x => !x.IsDeleted)
                .Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public async virtual Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync(x => !x.IsDeleted);
        }
    }
}
