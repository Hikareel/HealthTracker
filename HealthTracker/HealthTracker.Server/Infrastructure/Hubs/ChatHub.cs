using AutoMapper;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChatHub(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task SendMessageToUser(int userFrom, int userTo, string messageText)
        {
            var message = new Message
            {
                UserIdTo = userTo,
                UserIdFrom = userFrom,
                Text = messageText
            };

            _context.Message.Add(message);
            await _context.SaveChangesAsync();

            await Clients.User(userFrom.ToString()).SendAsync("ReceiveMessage", userFrom, userTo, messageText);
            await Clients.User(userTo.ToString()).SendAsync("ReceiveMessage", userFrom, userTo, messageText);
        }



    }


}
