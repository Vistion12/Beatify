using Beatify.Database.Entities;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beatify.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SongsController(ISongRepository songRepository) : ControllerBase
{

    [HttpGet(Name = "GetAllSongs")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Song>))]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await songRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Song))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        if (!await songRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }

        var song = await songRepository.GetByIdAsync(id);
        return Ok(song);
    }

    [HttpPost(Name = "AddSong")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSong(Song song)
    {
        if (song == null)
        {
            return BadRequest();
        }
        if (await songRepository.ExistsByTitleAsync(song.Title))
        {
            ModelState.AddModelError("", "Already Exists");
            return StatusCode(StatusCodes.Status402PaymentRequired, ModelState);
        }
        await songRepository.AddAsync(song);
        return Ok();
    }

    [HttpDelete(Name = "RemoveSong")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveSong(int id)
    {
        if (!await songRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }
        await songRepository.RemoveAsync(id);
        return Ok();
    }

    [HttpPut(Name = "UpdateSong")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSong(int id, Song song)
    {
        if (!await songRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }
        await songRepository.UpdateAsync(id, song);
        return Ok();
    }
}
