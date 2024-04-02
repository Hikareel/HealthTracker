using AutoMapper;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;

namespace HealthTracker.Server.Modules.Community.Helpers
{
    public class ChatProfile : Profile
    {
        public ChatProfile() 
        {
            CreateMap<CreateMessageDTO, Message>();
            CreateMap<Message, MessageDTO>();
        }
    }
}
