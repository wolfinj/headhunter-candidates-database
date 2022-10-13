using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Data;
using Microsoft.EntityFrameworkCore;

namespace CatchSmartHeadHunter.Services;

public class DbService : IDbService
{
    protected HhDbContext Context;

    public DbService(HhDbContext context)
    {
        Context = context;
    }

    public void Create<T>(T entity) where T : Entity
    {
        Context.Set<T>().Add(entity);
        Context.SaveChanges();
    }

    public void Delete<T>(T entity) where T : Entity
    {
        Context.Set<T>().Remove(entity);
        Context.SaveChanges();
    }

    public void Update<T>(T entity) where T : Entity
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
    }

    public T? GetById<T>(int id) where T : Entity
    {
        var result = Context.Set<T>().SingleOrDefault(e => e.Id == id);
        return result;
    }

    public ICollection<T> GetAll<T>() where T : Entity
    {
        return Context.Set<T>().ToList();
    }

    public IQueryable<T> Query<T>() where T : Entity
    {
        return Context.Set<T>().AsQueryable();
    }
}
