using HomeMovieLibrary.Api.Extensions;
using HomeMovieLibrary.Api.Models;
using HomeMovieLibrary.Api.Models.DB;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HomeMovieLibrary.Api.Repositories;

public class GenericCRUDRepository<T> where T : IdName
{
    private readonly MovieLibraryDbContext _dbContext;
    private readonly IMapper _mapper;

    public GenericCRUDRepository(MovieLibraryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedResult<TResult>> GetMultipleAsync<TResult>(Filter filter = null, CancellationToken token = default) where TResult : class
    {
        PagedResult<TResult> result = new()
        {
            TotalCount = await _dbContext.Set<T>().CountAsync(token),
            PageIndex = filter?.PageIndex ?? 0,
            PageSize = filter?.PageSize ?? Constants.MaximumPageSize
        };

        var query = _dbContext.Set<T>()
            .AsQueryable()
            .ApplyFilter(filter);

        var filteredCount = await query.CountAsync(token);

        var data = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(token);

        result.Data = _mapper.Map<List<TResult>>(data);
        result.FilteredCount = filteredCount;
        return result;
    }

    public async Task<TResult> GetByIdAsync<TResult>(int id, CancellationToken token = default)
    {
        var result = await _dbContext.Set<T>()
            .AsNoTracking()
            .IncludeAllProperties()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        if (result == null) return default;

        return _mapper.Map<TResult>(result);
    }

    public async Task<TResult> AddAsync<TResult, TSource>(TSource form, CancellationToken token = default)
    {
        var result = _mapper.Map<T>(form);

        _dbContext.Set<T>().Add(result);

        await _dbContext.SaveChangesAsync(token);

        return await GetByIdAsync<TResult>(result.Id, token);
    }

    public async Task<TResult> UpdateAsync<TResult, TSource>(TSource form, CancellationToken token = default) where TSource : IUpdateForm
    {
        var result = await _dbContext.Set<T>()
            .FirstOrDefaultAsync(x => x.Id == 1, token);

        if (result == null) return default;

        _mapper.Map(form, result);

        await _dbContext.SaveChangesAsync(token);

        return await GetByIdAsync<TResult>(result.Id, token);
    }

    public async Task<TResult> DeleteAsync<TResult>(int id, CancellationToken token = default)
    {
        var dbResult = await _dbContext.Set<T>()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        if (dbResult == null) return default;

        var result = _mapper.Map<TResult>(dbResult);

        _dbContext.Set<T>().Remove(dbResult);
        await _dbContext.SaveChangesAsync(token);

        return result;
    }
}
