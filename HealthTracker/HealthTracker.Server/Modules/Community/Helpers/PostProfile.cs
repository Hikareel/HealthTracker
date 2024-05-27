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
            CreateMap<Comment, CommentDTO>();

            CreateMap<Like, LikeDTO>();
            CreateMap<LikeDTO, Like>();

        }
    }
}
