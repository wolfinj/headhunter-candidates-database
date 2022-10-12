using CatchSmartHeadHunter.Core.Models;

namespace CatchSmartHeadHunter.Core.Services;

public interface IEntityService<T> where T : Entity
{
    void Create(T entity);
    void Delete(T entity);
    void Update(T entity);
    ICollection<T> GetAll();
    T? GetById(int id);
    IQueryable<T> Query();
}
