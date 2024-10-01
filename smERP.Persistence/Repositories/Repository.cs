using Microsoft.EntityFrameworkCore;
using smERP.Domain.Entities;
using smERP.Persistence.Contracts;
using smERP.Persistence.Data;
using smERP.SharedKernel.Bases;
using System.Linq.Expressions;

namespace smERP.Persistence.Repositories;

public class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext _context = context;

    public virtual async Task<TEntity> GetByID(int ID)
    {
        return await _context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(x => x.Id == ID);
    }

    public virtual async Task<IEnumerable<TEntity>> GetListByIds(IEnumerable<int> Ids)
    {
        return await _context.Set<TEntity>().Where(x => Ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public IQueryable<TEntity> FilterData(Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc, BaseFiltration parameters)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        query = filterFunc(query);

        if (!string.IsNullOrEmpty(parameters.SearchText))
        {
            query = query.Where(b => EF.Property<string>(b, "Name").Contains(parameters.SearchText));
        }

        if (!string.IsNullOrEmpty(parameters.FromDate) && DateTime.TryParse(parameters.FromDate, out var fromDate))
        {
            query = query.Where(f => EF.Property<DateTime>(f, "CreatedDate").Date >= fromDate.Date);
        }

        if (!string.IsNullOrEmpty(parameters.ToDate) && DateTime.TryParse(parameters.ToDate, out var toDate))
        {
            query = query.Where(f => EF.Property<DateTime>(f, "CreatedDate").Date <= toDate.Date);
        }

        // Apply paging directly to the query
        //var pagedData = query
        //    .Skip(parameters.Start)
        //    .Take(parameters.Length)
        //    .ToList();

        return query;
    }

    public async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    //public async Task Hide(int ID)
    //{
    //    var entityToBeHidden = await _context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(x => x.Id == ID);
    //    if (entityToBeHidden is not null)
    //        entityToBeHidden.IsHidden = true;
    //}

    public async Task<bool> DoesExist(int ID)
    {
        return await _context.Set<TEntity>().AnyAsync(x => x.Id == ID);
    }

    public async Task<bool> DoesExist(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate);
    }

    public async Task<bool> IsTableEmpty()
    {
        return !await _context.Set<TEntity>().AsNoTracking().AnyAsync();
    }

    public async Task<int> CountExisting(IEnumerable<int> IDs)
    {
        return await _context.Set<TEntity>().CountAsync(x => IDs.Contains(x.Id));
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
}

