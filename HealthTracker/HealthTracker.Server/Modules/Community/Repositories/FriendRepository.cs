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

    }
}
