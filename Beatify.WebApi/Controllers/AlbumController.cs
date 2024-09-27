using Beatify.Database.Entities;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beatify.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController(IAlbumRepository albumRepository) : ControllerBase
{

    [HttpGet(Name = "GetAllAlbums")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Album>))]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await albumRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Album))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (!await albumRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }

        var album = await albumRepository.GetByIdAsync(id);
        return Ok(album);
    }

    [HttpPost(Name = "AddAlbum")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAlbum(Album album)
    {
        if (album == null)
        {
            return BadRequest();
        }
        if (await albumRepository.ExistsByTitleAsync(album.Title))
        {
            ModelState.AddModelError("", "Already Exists");
            return StatusCode(StatusCodes.Status402PaymentRequired, ModelState);
        }
        await albumRepository.AddAsync(album);
        return Ok();
    }

    [HttpDelete(Name = "RemoveAlbum")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveAlbum(Guid id)
    {
        if (!await albumRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }
        await albumRepository.RemoveAsync(id);
        return Ok();
    }

    [HttpPut(Name = "UpdateAlbum")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAlbum(Guid id, Album album)
    {
        if (!await albumRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }
        await albumRepository.UpdateAsync(id, album);
        return Ok();
    }
}
