using AutoMapper;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;

namespace HealthTracker.Server.Modules.Community.Helpers
{
    public class PostProfile : Profile
    {
        public PostProfile() 
        {
            CreateMap<CreatePostDTO, Post>();
            CreateMap<Post, PostDTO>();

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName));

            CreateMap<Like, LikeDTO>();
            CreateMap<LikeDTO, Like>();

        }
    }
}
