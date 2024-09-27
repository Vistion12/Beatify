using Beatify.Database.Entities;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beatify.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController(IGenreRepository genreRepository) : ControllerBase
{
    [HttpGet(Name = "GetAllGenres")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Genre>))]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await genreRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Genre))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        if (!await genreRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }

        var genre = await genreRepository.GetByIdAsync(id);
        return Ok(genre);
    }

    [HttpPost(Name = "AddGenre")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGenre(Genre genre)
    {
        if (genre == null)
        {
            return BadRequest();
        }
        if (await genreRepository.ExistsByTitleAsync(genre.Title))
        {
            ModelState.AddModelError("", "Already Exists");
            return StatusCode(StatusCodes.Status402PaymentRequired, ModelState);
        }
        await genreRepository.AddAsync(genre);
        return Ok();
    }

    [HttpDelete(Name = "RemoveGenre")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveGenre(int id)
    {
        if (!await genreRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }
        await genreRepository.RemoveAsync(id);
        return Ok();
    }

    [HttpPut(Name = "UpdateGenre")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGenre(int id, Genre genre)
    {
        if (!await genreRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }
        await genreRepository.UpdateAsync(id, genre);
        return Ok();
    }
}
