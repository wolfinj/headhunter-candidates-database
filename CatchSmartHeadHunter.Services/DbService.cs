using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Data;
using Microsoft.EntityFrameworkCore;

namespace CatchSmartHeadHunter.Services;

public class DbService : IDbService
{
    protected HhDbContext _context;

    public DbService(HhDbContext context)
    {
        _context = context;
    }

    public void Create<T>(T entity) where T : Entity
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete<T>(T entity) where T : Entity
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public void Update<T>(T entity) where T : Entity
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public T? GetById<T>(int id) where T : Entity
    {
        var result = _context.Set<T>().SingleOrDefault(e => e.Id == id);
        return result;
    }

    public ICollection<T> GetAll<T>() where T : Entity
    {
        return _context.Set<T>().ToList();
    }

    public IQueryable<T> Query<T>() where T : Entity
    {
        return _context.Set<T>().AsQueryable();
    }
}
