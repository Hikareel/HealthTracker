using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.EntityFrameworkCore;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
        Task<FriendshipDTO> CreateFriendshipRequest(CreateFriendshipDTO createFriendshipDTO);
        Task<FriendshipDTO> GetFriendship(int friendshipId);
        Task ChangeFriendshipStatus(int userId, int friendId, bool isAccepted);
        Task DeleteFriendship(int userId, int friendId);
    }

    public class FriendshipRepository : IFriendRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStatusRepository _statusRepository;
        public FriendshipRepository(ApplicationDbContext context, IMapper mapper, IStatusRepository statusRepository)
        {
            _context = context;
            _mapper = mapper;
            _statusRepository = statusRepository;
        }
        //Jeśli już jest to albo usuwasz poprzedni i tworzysz nowy albo zmieniasz status albo jeszcze inaczej - do przegadania i zaimplementowania
        public async Task<FriendshipDTO> CreateFriendshipRequest(CreateFriendshipDTO createFriendshipDTO)
        {

            bool friendshipExists = await _context.Friendship.AnyAsync(f =>
                (f.User1Id == createFriendshipDTO.User1Id && f.User2Id == createFriendshipDTO.User2Id) ||
                (f.User1Id == createFriendshipDTO.User2Id && f.User2Id == createFriendshipDTO.User1Id));

            if (friendshipExists)
            {
                throw new InvalidOperationException("Friendship already exists.");
            }
            Status status = await _statusRepository.GetStatus("RequestSend");

            var friendship = _mapper.Map<Friendship>(createFriendshipDTO);
            friendship.Status = status;

            await _context.Friendship.AddAsync(friendship);
            await _context.SaveChangesAsync();

            return _mapper.Map<FriendshipDTO>(friendship);
        }

        public async Task<FriendshipDTO> GetFriendship(int friendshipId)
        {
            var friendship = await _context.Friendship
                .Where(line => line.Id == friendshipId)
                .ProjectTo<FriendshipDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return friendship;
        }

        public async Task ChangeFriendshipStatus(int userId, int friendId, bool isAccepted)
        {
            Status status = await _statusRepository.GetStatus(isAccepted ? "Accepted" : "Declined");

            var friendship = await _context.Friendship
                .Where(f => (f.User1Id == userId && f.User2Id == friendId) || (f.User2Id == userId && f.User1Id == friendId))
                .ToListAsync();
            foreach (var item in friendship)
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
