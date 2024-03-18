using HealthTracker.Server.Infrastructure.Repositories;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.EntityFrameworkCore;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Core.Models;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    public interface IFriendRepository
    {
        Task<FriendshipListDTO> GetFriendList(int id);
        Task<FriendshipListDTO> GetFriend(int id);
    }

    public class FriendRepository : IFriendRepository
    {
        private readonly ApplicationDbContext _context;
        public FriendRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FriendshipListDTO> GetFriendList(int id)
        {
            var friends = await _context.Friendship
                .Where(f => f.User1Id == id || f.User2Id == id) //Status!
                .Select(f => new
                {
                    User = f.User1Id == id ? f.User2 : f.User1
                })
                .Distinct()
                .Select(u => new FriendDTO
                {
                    UserId = u.User.Id,
                    FirstName = u.User.FirstName,
                    LastName = u.User.LastName
                })
                .ToListAsync();

            return new FriendshipListDTO { Friends = friends };
        }

        public async Task<FriendDTO?> GetFriend(int id)
        {
            var friend = await _context.User
                .Select(f => new FriendDTO
                {
                    UserId = f.Id,
                    FirstName = f.FirstName,
                    LastName = f.LastName
                })
                .FirstOrDefaultAsync();

            var res = _context.Friendship.Where(f => (f.User1Id == id && f.User2Id == friend.UserId) || (f.User2Id == id && f.User1Id == friend.UserId)).Any();

            return res ? friend : null;
        }
    }
}
