

namespace Application.interfaces;
public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task SaveChangesAsync();
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> Query();
}