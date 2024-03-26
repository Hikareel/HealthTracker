using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    public interface IChatRepository 
    {
        Task<ChatMessagesDTO> GetMessages(int userFrom, int userTo, int pageNumber, int pageSize);
        Task SendMessage(SendMessageDTO sendMessageDTO);
    }
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ChatMessagesDTO> GetMessages(int userFrom, int userTo, int pageNumber, int pageSize)
        {
            var messages = await _context.Message
                .Where(m => (m.UserIdFrom == userFrom && m.UserIdTo == userTo) || (m.UserIdFrom == userTo && m.UserIdTo == userFrom))
                .OrderByDescending(m => m.SendTime)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(m => new MessageDTO
                {
                    IsUserMessage = userFrom == m.UserIdFrom ? true : false,
                    Text = m.Text,
                })
                .ToListAsync();

            return new ChatMessagesDTO { UserTo = userTo, UserFrom = userFrom, Messages = messages };

        }

        public async Task SendMessage(SendMessageDTO sendMessageDTO)
        {

            var mess = new Message()
            {
                UserIdFrom = sendMessageDTO.UserIdFrom,
                UserIdTo = sendMessageDTO.UserIdTo,
                Text = sendMessageDTO.Text,
            };
            await _context.Message.AddAsync(mess);
            await _context.SaveChangesAsync();

        }
    }
}
