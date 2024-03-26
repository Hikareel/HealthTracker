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

        // POST newFriend
        [HttpPost("users/{userId}/friends/{friendId}")]
        public async Task<ActionResult<bool>> FriendshipRequest(int userId, int friendId)
        {
            try
            {
                await _friendRepository.CreateFriendshipRequest(userId, friendId);
                return true;
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT change friendship status
        [HttpPut("users/{userId}/friends/{friendId}/{isAccepted}")]
        public async Task<ActionResult<bool>> Put(int userId, int friendId, bool isAccepted)
        {
            try
            {
                await _friendRepository.ChangeFriendshipStatus(userId, friendId, isAccepted);
                return true;
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //// DELETE api/<FriendshipController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
