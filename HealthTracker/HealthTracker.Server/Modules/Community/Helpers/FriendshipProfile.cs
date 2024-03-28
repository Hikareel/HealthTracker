using AutoMapper;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;

namespace HealthTracker.Server.Modules.Community.Helpers
{
    public class FriendshipProfile : Profile
    {
        public FriendshipProfile()
        {
            CreateMap<CreateFriendshipDTO, Friendship>();
            CreateMap<Friendship, FriendshipDTO>();
        }
    }
}
