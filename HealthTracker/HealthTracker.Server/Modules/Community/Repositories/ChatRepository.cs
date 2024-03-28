using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
            try
            {
                var mess = _mapper.Map<Message>(sendMessageDTO);

                await _context.Message.AddAsync(mess);
                await _context.SaveChangesAsync();

                return _mapper.Map<MessageDTO>(mess);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<MessageDTO> GetMessage(int Id)
        {
            var message = await _context.Message
                .Where(line => line.Id == Id)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return message;
        }

        public async Task<List<MessageDTO>> GetMessages(int userFrom, int userTo, int pageNumber, int pageSize)
        {
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
