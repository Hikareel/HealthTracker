using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    public interface IPostRepository
    {
        Task<PostDTO> CreatePost(CreatePostDTO postDTO);
        Task<PostDTO> GetPost(int postId);
        Task DeletePost(int postId);
        Task<List<PostDTO>> GetPosts(int UserId, int pageSize, int pageNumber);
        Task<CommentDTO> CreateComment(int? parentCommentId, CreateCommentDTO commentDTO);
        Task<CommentDTO> GetComment(int commentId);
        Task<List<CommentDTO>> GetCommentsByPostId(int postId);
        Task DeleteComment(int commentId);
        Task<LikeDTO> CreateLike(CreateLikeDTO likeDTO);
        Task<LikeDTO> GetLike(int likeId);
        Task DeleteLike(int likeId);
        Task DeleteUsersLike(int userId, int postId);
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

        public async Task DeletePost(int postId)
        {
            var post = await _context.Post.FindAsync(postId);

            if (post == null)
            {
                throw new PostNotFoundException();
            }

            _context.Post.Remove(post);

            await _context.SaveChangesAsync();

        }

        public async Task<List<PostDTO>> GetPosts(int userId, int pageSize, int pageNumber)
        {
            if (!await _context.User.AnyAsync(u => u.Id == userId))
            {
                throw new UserNotFoundException();
            }

            Status status = await _statusRepository.GetStatus("Accepted");
            var friendIds = await _context.Friendship
                .Where(f => (f.User1Id == userId || f.User2Id == userId) && f.StatusId == status.Id)
                .Select(f => f.User1Id == userId ? f.User2Id : f.User1Id)
                .Distinct()
                .ToListAsync();

            var posts = await _context.Post
                .Where(p => friendIds.Contains(p.UserId))
                .OrderByDescending(p => p.DateOfCreate)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(p => new PostDTO
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    UserFirstName = p.User.FirstName,
                    UserLastName = p.User.LastName,
                    Content = p.Content,
                    DateOfCreate = p.DateOfCreate,
                    Comments = p.Comments.Select(c => _mapper.Map<CommentDTO>(c)).ToList(),
                    Likes = p.Likes.Select(l => _mapper.Map<LikeDTO>(l)).ToList()
                })
                .ToListAsync();

            return posts;
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

        //W przyszłości dodać paginację dla dużej ilości komentarzy!
        public async Task<List<CommentDTO>> GetCommentsByPostId(int postId)
        {
            var result = await _context.Comment.Where(line => line.PostId == postId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
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

        public async Task<LikeDTO> CreateLike(CreateLikeDTO likeDTO)
        {
            if(await _context.Like.AnyAsync(p => p.UserId == likeDTO.UserId && p.PostId == likeDTO.PostId))
            {
                throw new LikeAlreadyExistsException();
            }

            if (!await _context.User.AnyAsync(line => line.Id == likeDTO.UserId))
            {
                throw new UserNotFoundException();
            }

            if (!await _context.Post.AnyAsync(line => line.Id == likeDTO.PostId))
            {
                throw new PostNotFoundException();
            }

            var like = _mapper.Map<Like>(likeDTO);

            await _context.Like.AddAsync(like);
            await _context.SaveChangesAsync();

            return _mapper.Map<LikeDTO>(like);
        }

        public async Task<LikeDTO> GetLike(int likeId)
        {
            var like = await _context.Like
                .FirstOrDefaultAsync(p => p.Id == likeId);

            var likeDto = _mapper.Map<LikeDTO>(like);

            return likeDto ?? throw new LikeNotFoundException();
        }

        public async Task DeleteLike(int likeId)
        {
            var like = await _context.Like
                .FirstOrDefaultAsync(p => p.Id == likeId);
            if (like != null)
            {
                _context.Like.Remove(like);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new LikeNotFoundException();
            }
        }

        public async Task DeleteUsersLike(int userId, int postId)
        {
            var like = await _context.Like
                .FirstOrDefaultAsync(p => p.UserId == userId && p.PostId == postId);
            if (like != null)
            {
                _context.Like.Remove(like);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new LikeNotFoundException();
            }
        }
    }
}
