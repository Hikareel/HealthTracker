using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    public interface IPostRepository
    {
        Task<PostDTO> CreatePost(PostDTO postDTO);
        Task<PostListDTO> GetPosts(int UserId, int pageSize, int pageNumber);
    }
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PostDTO> CreatePost(PostDTO postDTO)
        {
            try
            {
                var post = new Post()
                {
                    Content = postDTO.Content,
                    UserId = postDTO.UserId
                };

                await _context.Post.AddAsync(post);
                await _context.SaveChangesAsync();
                return new PostDTO()
                {
                    Content = post.Content,
                    UserId = post.UserId,
                };
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<PostListDTO> GetPosts(int UserId, int pageSize, int pageNumber)
        {
            var friends = await _context.Friendship
                .Where(f => f.User1Id == UserId || f.User2Id == UserId) //Status!
                .Select(f => new
                {
                    User = f.User1Id == UserId ? f.User2 : f.User1
                })
                .Distinct()
                .Select(u => u.User.Id)
                .ToListAsync();

            var posts = await _context.Post
                .Where(post => friends.Contains(post.UserId))
                .Select(u => new PostDTO
                {
                    UserId = u.UserId,
                    Content = u.Content,
                    DateOfCreate = u.DateOfCreate
                })
                .OrderByDescending(u => u.DateOfCreate)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();


            return new PostListDTO
            {
                UserId = UserId,
                Posts = posts
            };
        }
    }
}
