using HealthTracker.Server.Infrastructure.Repositories;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.EntityFrameworkCore;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    /// <summary>
    /// Status: Name
    /// RequestSend
    /// Accepted / Declined
    /// </summary>
    public interface IFriendRepository
    {
        Task<List<FriendDTO>> GetFriendList(int id);
        Task CreateFriendshipRequest(int userId, int freindId);
        Task ChangeFriendshipStatus(int userId, int friendId, bool isAccepted);
        Task DeleteFriendship(int userId, int friendId);
    }

    public class FriendshipRepository : IFriendRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStatusRepository _statusRepository;
        public FriendshipRepository(ApplicationDbContext context, IStatusRepository statusRepository)
        {
            _context = context;
            _statusRepository = statusRepository;
        }

        public async Task CreateFriendshipRequest(int userId, int freindId)
        {
            Status status = await _statusRepository.GetStatus("RequestSend");

            var friendship = new Friendship()
            {
                User1Id = userId,
                User2Id = freindId,
                StatusId = status.Id,
                DateOfStart = null
            };

            await _context.Friendship.AddAsync(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeFriendshipStatus(int userId, int friendId, bool isAccepted)
        {
            Status status = await _statusRepository.GetStatus(isAccepted ? "Accepted" : "Declined");

            var friendship = await _context.Friendship
                .Where(f => (f.User1Id == userId && f.User2Id == friendId) || (f.User2Id == userId && f.User1Id == friendId))
                .ToListAsync();
            foreach(var item in friendship)
            {
                item.Status = status;
                item.DateOfStart = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<FriendDTO>> GetFriendList(int id)
        {
            Status status = await _statusRepository.GetStatus("Accepted");
            var friends = await _context.Friendship
                .Where(f => f.User1Id == id || f.User2Id == id)
                .Where(f => f.Status == status)
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

            return friends;
        }

        public async Task DeleteFriendship(int userId, int friendId)
        {
            var friendships = await _context.Friendship
                .Where(f => (f.User1Id == userId && f.User2Id == friendId) || (f.User2Id == userId && f.User1Id == friendId))
                .ToListAsync();

            _context.Friendship.RemoveRange(friendships);
            await _context.SaveChangesAsync();
        }

    }
}
