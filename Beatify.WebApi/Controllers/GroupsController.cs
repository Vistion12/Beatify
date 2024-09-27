using Beatify.Database.Entities;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beatify.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController(IGroupRepository groupRepository) : ControllerBase
{
    [HttpGet(Name = "GetAllGroups")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Group>))]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await groupRepository.GetAllAsync());
    }


    [HttpGet( "{id}")]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(Group))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task <IActionResult> GetById(int id)
    {
        if(!await groupRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }

        var group= await groupRepository.GetByIdAsync(id);
        return Ok(group);
    }


    [HttpPost(Name = "AddGroup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGroup(Group group)
    {

        if (group == null)
        {
            return BadRequest();
        }
        if(await groupRepository.ExistsByTitleAsync(group.Title))
        {
            ModelState.AddModelError("", "Already Exists");
            return StatusCode(StatusCodes.Status402PaymentRequired, ModelState);
        }    
        await groupRepository.AddAsync(group);
        return Ok();
    }

    [HttpDelete(Name = "RemoveGroup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveGroup(int id)
    {
        if (!await groupRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }
        await groupRepository.RemoveAsync(id);
        return Ok();
    }

    [HttpPut(Name = "UpdateGroup")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGroup(int id,Group group)
    {
        if (!await groupRepository.ExistsByIdAsync(id))
        {
            return NotFound();
        }
        await groupRepository.UpdateAsync(id,group);
        return Ok();
    }
}
