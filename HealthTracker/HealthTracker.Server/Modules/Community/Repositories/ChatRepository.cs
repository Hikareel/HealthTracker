using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    public interface IChatRepository
    {
        Task<ChatMessagesDTO> GetMessages(int userTo, int userFrom, int pageNumber, int pageSize);
        Task<bool> SendMessage(SendMessageDTO sendMessageDTO);
    }
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ChatMessagesDTO> GetMessages(int userTo, int userFrom, int pageNumber, int pageSize)
        {
            var messages = await _context.Message
                .Where(m => (m.UserIdFrom == userFrom && m.UserIdTo == userTo) || (m.UserIdFrom == userTo && m.UserIdTo == userFrom))
                .OrderByDescending(m => m.SendTime)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return messages == null ? new ChatMessagesDTO() : new ChatMessagesDTO { UserTo = userTo, UserFrom = userFrom, Messages = messages, CurrentLastMessageId = messages.Last().Id };

        }

        public async Task<bool> SendMessage(SendMessageDTO sendMessageDTO)
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
