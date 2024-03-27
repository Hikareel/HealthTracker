using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Design;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    public interface IPostRepository
    {
        Task<PostDTO> CreatePost(CreatePostDTO postDTO);
        Task<PostDTO> GetPost(int postId);
        Task<PostListDTO> GetPosts(int UserId, int pageSize, int pageNumber);
        Task<CommentDTO> CreateComment(int? parentCommentId, CreateCommentDTO commentDTO);
        Task<CommentDTO> GetComment(int commentId);
        Task<List<CommentDTO>> GetCommentsByPostId(int postId);
        Task DeleteComment(int commentId);
    }
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStatusRepository _statusRepository;
        public PostRepository(ApplicationDbContext context, IStatusRepository statusRepository)
        {
            _context = context;
            _statusRepository = statusRepository;
        }

        public async Task<PostDTO> CreatePost(CreatePostDTO postDTO)
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
                    Id = post.Id,
                    Content = post.Content,
                    DateOfCreate = post.DateOfCreate,
                    UserId = post.UserId,
                };
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<PostDTO> GetPost(int postId)
        {
            var post = await _context.Post
                .Where(post => post.Id == postId)
                .Select(u => new PostDTO
                {
                    Id = u.Id,
                    UserId = u.UserId,
                    Content = u.Content,
                    DateOfCreate = u.DateOfCreate
                })
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task<PostListDTO> GetPosts(int UserId, int pageSize, int pageNumber)
        {
            Status status = await _statusRepository.GetStatus("Accepted");
            var friends = await _context.Friendship
                .Where(f => f.User1Id == UserId || f.User2Id == UserId)
                .Where(f => f.StatusId == status.Id)
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

        public async Task<CommentDTO> CreateComment(int? parentCommentId, CreateCommentDTO commentDTO)
        {
            try
            {
                var comment = new Comment()
                {
                    Content = commentDTO.Content,
                    UserId = commentDTO.UserId,
                    PostId = commentDTO.PostId,
                    ParentCommentId = parentCommentId.HasValue ? parentCommentId : null,
                };
                await _context.Comment.AddAsync(comment);
                await _context.SaveChangesAsync();

                return new CommentDTO()
                {
                    Content = comment.Content,
                    Id = comment.Id,
                    ParentCommentId = comment.ParentCommentId,
                    PostId = comment.PostId,
                    UserId = comment.UserId,
                };
            }
            catch(Exception)
            {
                return null;
            }
            
        }

        public async Task<CommentDTO> GetComment(int commentId)
        {
            var comment = await _context.Comment
                .Where(line => line.Id == commentId)
                .Select(u => new CommentDTO()
                {
                    Content = u.Content,
                    Id = u.Id,
                    ParentCommentId = u.ParentCommentId.HasValue ? u.ParentCommentId : null,
                    PostId = u.PostId,
                    UserId = u.UserId
                })
                .FirstOrDefaultAsync();
            return comment;
        }

        //W przyszłości dodać paginację dla dużej ilości komentarzy!
        public async Task<List<CommentDTO>> GetCommentsByPostId(int postId)
        {
            var result = await _context.Comment.Where(line => line.PostId == postId)
                .Select(f => new CommentDTO()
                {
                    Id = f.Id,
                    Content = f.Content,
                    UserId = f.UserId,
                    PostId = f.PostId,
                    ParentCommentId = f.ParentCommentId.HasValue ? f.ParentCommentId : null,
                })
                .ToListAsync();
            return result;
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await _context.Comment.FindAsync(commentId);
            if (comment != null)
            {
                await DeleteChildComments(comment.Id);
                _context.Comment.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        private async Task DeleteChildComments(int parentId)
        {
            var childComments = await _context.Comment.Where(c => c.ParentCommentId == parentId).ToListAsync();
            foreach (var child in childComments)
            {
                await DeleteChildComments(child.Id);
            }
            _context.Comment.RemoveRange(childComments);
        }

    }
}
