using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Resume_Analyzer_Backend.Models;
using System.Linq.Expressions;

namespace Resume_Analyzer_Backend.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetItemWithConditionAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        protected DbSet<T> dbSet;

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            dbSet = _unitOfWork.Context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetItemWithConditionAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the entity.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the entity.", ex);
            }

        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the entity.", ex);
            }
        }
    }

    public class UserManagement : RepositoryBase<User>
    {
        public UserManagement(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
