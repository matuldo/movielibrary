using HomeMovieLibrary.Api.Models;
using HomeMovieLibrary.Api.Models.DB;
using HomeMovieLibrary.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeMovieLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly GenericCRUDRepository<Author> _repository;

    public AuthorsController(GenericCRUDRepository<Author> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PagedResult<AuthorFlat>> Get([FromQuery] Filter queryFilter, CancellationToken token)
    {
        return await _repository.GetMultipleAsync<AuthorFlat>(queryFilter, token);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<AuthorFull> Get(int id, CancellationToken token)
    {
        var movie = await _repository.GetByIdAsync<AuthorFull>(id, token);

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
    public async Task<AuthorFull> Post([FromBody] AuthorCreateForm form, CancellationToken token)
    {
        Response.StatusCode = StatusCodes.Status201Created;
        return await _repository.AddAsync<AuthorFull, AuthorCreateForm>(form, token);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<AuthorFull> Put(int id, [FromBody] AuthorUpdateForm form, CancellationToken token)
    {
        var existing = await _repository.GetByIdAsync<AuthorFlat>(id, token);
        if (existing is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return default;
        }

        Response.StatusCode = StatusCodes.Status200OK;
        return await _repository.UpdateAsync<AuthorFull, AuthorUpdateForm>(form, token);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<AuthorFull> Delete(int id, CancellationToken token)
    {
        var existing = await _repository.GetByIdAsync<AuthorFlat>(id, token);

        if (existing is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return default;
        }

        Response.StatusCode = StatusCodes.Status200OK;
        return await _repository.DeleteAsync<AuthorFull>(id, token);
    }
}
