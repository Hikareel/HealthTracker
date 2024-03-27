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
        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MessageDTO> CreateMessage(CreateMessageDTO sendMessageDTO)
        {
            try
            {
                var mess = new Message()
                {
                    UserIdFrom = sendMessageDTO.UserIdFrom,
                    UserIdTo = sendMessageDTO.UserIdTo,
                    Text = sendMessageDTO.Text,
                };
                await _context.Message.AddAsync(mess);
                await _context.SaveChangesAsync();

                return new MessageDTO()
                {
                    Id = mess.Id,
                    UserIdFrom = mess.UserIdFrom,
                    UserIdTo = mess.UserIdTo,
                    Text = mess.Text
                };
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
                .Select(u => new MessageDTO() 
                {
                    Id = u.Id,
                    UserIdFrom = u.UserIdFrom,
                    UserIdTo = u.UserIdTo,
                    Text = u.Text
                })
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
                .Select(m => new MessageDTO
                {
                    Id = m.Id,
                    UserIdFrom = m.UserIdFrom,
                    UserIdTo = m.UserIdTo,
                    Text = m.Text
                })
                .ToListAsync();

            return messages;

        }

    }
}
