using HomeMovieLibrary.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using HomeMovieLibrary.Api.Models;
using HomeMovieLibrary.Api.Models.DB;

namespace HomeMovieLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly GenericCRUDRepository<Movie> _repository;

    public MoviesController(GenericCRUDRepository<Movie> repository)
    {
        _repository = repository;
    }
        
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PagedResult<MovieFlat>> Get([FromQuery] Filter queryFilter, CancellationToken token)
    {            
        return await _repository.GetMultipleAsync<MovieFlat>(queryFilter, token);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<MovieFull> Get(int id, CancellationToken token)
    {
        var movie = await _repository.GetByIdAsync<MovieFull>(id, token);

        if (movie is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return default;
        }

        return movie;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<MovieFull> Post([FromBody] MovieCreateForm form, CancellationToken token)
    {
        Response.StatusCode = StatusCodes.Status201Created;
        return await _repository.AddAsync<MovieFull, MovieCreateForm>(form, token);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<MovieFull> Put(int id, [FromBody] MovieUpdateForm form, CancellationToken token)
    {
        var existing = await _repository.GetByIdAsync<MovieFlat>(id, token);
        if (existing is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return default;
        }

        Response.StatusCode = StatusCodes.Status200OK;
        return await _repository.UpdateAsync<MovieFull, MovieUpdateForm>(form, token);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<MovieFull> Delete(int id, CancellationToken token)
    {
        var existing = await _repository.GetByIdAsync<MovieFlat>(id, token);

        if (existing is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return default;
        }

        Response.StatusCode = StatusCodes.Status200OK;
        return await _repository.DeleteAsync<MovieFull>(id, token);
    }
}
