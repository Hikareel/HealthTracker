using AutoMapper;
using HealthTracker.Server.Core.DTOs;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;

namespace HealthTracker.Server.Modules.Community.Helpers
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, SuccessLoginDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

        }
    }
}
