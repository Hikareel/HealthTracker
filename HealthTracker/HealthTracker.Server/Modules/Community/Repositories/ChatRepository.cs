using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    public interface IChatRepository 
    {
        Task<MessageDTO> CreateMessage(CreateMessageDTO sendMessageDTO);
        Task<MessageDTO> GetMessage(int Id);
        Task<List<MessageDTO>> GetMessages(int userFrom, int userTo, int pageNumber, int pageSize);
        
    }
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ChatRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MessageDTO> CreateMessage(CreateMessageDTO sendMessageDTO)
        {
            var user1 = await _context.User.AnyAsync(line => line.Id == sendMessageDTO.UserIdFrom);
            var user2 = await _context.User.AnyAsync(line => line.Id == sendMessageDTO.UserIdTo);

            if(!user1 || !user2)
            {
                throw new UserNotFoundException(user1 ? sendMessageDTO.UserIdFrom : sendMessageDTO.UserIdTo);
            }

            var mess = _mapper.Map<Message>(sendMessageDTO);

            await _context.Message.AddAsync(mess);
            await _context.SaveChangesAsync();

            return _mapper.Map<MessageDTO>(mess);           
        }

        public async Task<MessageDTO> GetMessage(int id)
        {
            var message = await _context.Message
                .Where(line => line.Id == id)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return message ?? throw new MessageNotFoundException(id);
        }

        public async Task<List<MessageDTO>> GetMessages(int userFrom, int userTo, int pageNumber, int pageSize)
        {
            var user1 = await _context.User.AnyAsync(line => line.Id == userFrom);
            var user2 = await _context.User.AnyAsync(line => line.Id == userTo);

            if (!user1 || !user2)
            {
                throw new UserNotFoundException(user1 ? userFrom : userTo);
            }

            var messages = await _context.Message
                .Where(m => (m.UserIdFrom == userFrom && m.UserIdTo == userTo) || (m.UserIdFrom == userTo && m.UserIdTo == userFrom))
                .OrderByDescending(m => m.SendTime)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return messages;

        }

    }
}
