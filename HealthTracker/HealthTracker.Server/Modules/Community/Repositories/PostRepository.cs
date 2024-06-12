using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.Community;
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
        Task DeletePost(int postId);
        Task<PostListDTO> GetPosts(int UserId, int pageSize, int pageNumber);
        Task<CommentDTO> CreateComment(int? parentCommentId, CreateCommentDTO commentDTO);
        Task<CommentDTO> GetComment(int commentId);
        Task<CommentFromPostDTO> GetCommentsByPostId(int postId, int pageNr, int pageSize);
        Task DeleteComment(int commentId);
        Task DeleteCommentsFromPost(int postId);
        Task DeleteUserComments(int userId);
    }
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStatusRepository _statusRepository;

        public PostRepository(ApplicationDbContext context, IMapper mapper, IStatusRepository statusRepository)
        {
            _context = context;
            _mapper = mapper;
            _statusRepository = statusRepository;
        }

        public async Task<PostDTO> CreatePost(CreatePostDTO postDTO)
        {
            if (!await _context.User.AnyAsync(line => line.Id == postDTO.UserId))
            {
                throw new UserNotFoundException();
            }

            var post = _mapper.Map<Post>(postDTO);

            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();

            return _mapper.Map<PostDTO>(post);
        }

        public async Task<PostDTO> GetPost(int postId)
        {
            var post = await _context.Post
                .FirstOrDefaultAsync(p => p.Id == postId);

            var postDto = _mapper.Map<PostDTO>(post);

            return postDto ?? throw new PostNotFoundException();
        }

        public async Task DeletePost(int postId) //Usuwanie komentarzy + lików 
        {
            var post = await _context.Post.FindAsync(postId);
            
            if (post == null)
            {
                throw new PostNotFoundException();
            }

            _context.Post.Remove(post);

            await _context.SaveChangesAsync();

        }

        public async Task<PostListDTO> GetPosts(int userId, int pageSize, int pageNumber)
        {
            if (!await _context.User.AnyAsync(line => line.Id == userId))
            {
                throw new UserNotFoundException();
            }

            Status status = await _statusRepository.GetStatus("Accepted");
            var friendIds = await _context.Friendship
                .Where(f => f.User1Id == userId || f.User2Id == userId)
                .Where(f => f.StatusId == status.Id)
                .Select(f => f.User1Id == userId ? f.User2Id : f.User1Id)
                .Distinct()
                .ToListAsync();

            var postsQuery = _context.Post
                .Where(post => friendIds.Contains(post.UserId))
                .OrderByDescending(post => post.DateOfCreate);

            var posts = await postsQuery
                .ProjectTo<PostDTO>(_mapper.ConfigurationProvider)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            if (posts.Count == 0)
            {
                throw new NullPageException();
            }

            return new PostListDTO
            {
                UserId = userId,
                Posts = posts
            };
        }

        public async Task<CommentDTO> CreateComment(int? parentCommentId, CreateCommentDTO commentDTO)
        {
            if (parentCommentId.HasValue)
            {
                var parentComment = await GetComment(parentCommentId.Value);
                if (parentComment == null)
                {
                    throw new CommentNotFoundException();
                }
            }

            if (!await _context.User.AnyAsync(line => line.Id == commentDTO.UserId))
            {
                throw new UserNotFoundException();
            }

            if (!await _context.Post.AnyAsync(line => line.Id == commentDTO.PostId))
            {
                throw new PostNotFoundException();
            }

            var comment = _mapper.Map<Comment>(commentDTO);
            comment.ParentCommentId = parentCommentId;

            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<CommentDTO> GetComment(int commentId)
        {
            var comment = await _context.Comment
                .Where(line => line.Id == commentId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return comment ?? throw new CommentNotFoundException();
        }

        public async Task<CommentFromPostDTO> GetCommentsByPostId(int postId, int pageNr, int pageSize)
        {
            var totalCommentsCount = await _context.Comment
                .Where(comment => comment.PostId == postId)
                .CountAsync();

            if (totalCommentsCount == 0)
            {
                throw new NullPageException();
            }
            
            var comments = await _context.Comment
                .Where(comment => comment.PostId == postId)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new CommentFromPostDTO()
            {
                Comments = comments,
                PageNr = pageNr,
                PageSize = pageSize,
                PostId = postId,
                CommentsCount = totalCommentsCount
            };
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
            else
            {
                throw new CommentNotFoundException();
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

        public async Task DeleteCommentsFromPost(int postId)
        {
            if (!await _context.Post.AnyAsync(line => line.Id == postId))
            {
                throw new PostNotFoundException();
            }

            var comments = await _context.Comment
                .Where(line => line.PostId == postId)
                .ToListAsync();

            _context.RemoveRange(comments);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserComments(int userId)
        {
            if (!await _context.User.AnyAsync(line => line.Id == userId))
            {
                throw new UserNotFoundException();
            }

            var comments = await _context.Comment
                .Where(line=> line.UserId == userId)
                .ToListAsync();

            _context.RemoveRange(comments);
            await _context.SaveChangesAsync();
        }
    }
}
