using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthTracker.Server.Modules.Community.Controllers
{
    [Route("api")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        public readonly IFriendRepository _friendRepository;
        public FriendshipController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        // GET: api/<FriendshipController>
        [HttpGet("users/{id}/friends")]
        public async Task<ActionResult<FriendshipListDTO>> GetFriendList(int id)
        {
            try
            {
                var friendsListDto = await _friendRepository.GetFriendList(id);
                if (friendsListDto == null || friendsListDto.Friends.Count == 0)
                {
                    return NotFound();
                }
                return Ok(friendsListDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //// POST newFriend
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{

        //}

        //// PUT change freindship status
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{

        //}

        //// DELETE api/<FriendshipController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
