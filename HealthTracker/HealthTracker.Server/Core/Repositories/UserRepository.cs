using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.DTOs;
using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace HealthTracker.Server.Core.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUser(int id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var userDTO = await _context.Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ?? throw new UserNotFoundException(id);

            return userDTO;
        }
    }
}
