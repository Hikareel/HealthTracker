using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.Community;
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
        Task<PostListDTO> GetPosts(int UserId, int pageSize, int pageNumber);
        Task<CommentDTO> CreateComment(int? parentCommentId, CreateCommentDTO commentDTO);
        Task<CommentDTO> GetComment(int commentId);
        Task<CommentFromPostDTO> GetCommentsByPostId(int postId, int pageNr, int pageSize);
        Task<List<CommentDTO>> GetCommentsByParentCommentId(int postId, int parentCommentId);
        Task DeleteComment(int commentId);
        Task DeleteCommentsFromPost(int postId);
        Task DeleteUserComments(int userId);
        Task<LikeDTO> CreateLike(LikeDTO likeDTO);
        Task<LikeDTO> GetLike(int userId, int postId);
        Task<List<LikeDTO>> GetLikesFromPost(int postId);
        Task DeleteLike(int userId, int postId);
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

        public async Task<PostDTO> CreatePost(CreatePostDTO createPostDTO)
        {
            var user = await _context.User.FirstOrDefaultAsync(line => line.Id == createPostDTO.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var post = _mapper.Map<Post>(createPostDTO);

            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();

            var postDTO = _mapper.Map<PostDTO>(post);

            postDTO.UserFirstName = user.FirstName;
            postDTO.UserLastName= user.LastName;

            return postDTO;
        }

        public async Task<PostDTO> GetPost(int postId)
        {
            var post = await _context.Post
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == postId) ?? throw new PostNotFoundException();

            var postDto = _mapper.Map<PostDTO>(post);
            postDto.AmountOfComments = await _context.Comment.CountAsync(c => c.PostId == postId);

            return postDto;
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
                    AmountOfComments = p.Comments.Count(),
                    Comments = p.Comments.Where(c => c.ParentComment == null).Select(c => _mapper.Map<CommentDTO>(c)).ToList(),
                    Likes = p.Likes.Select(l => _mapper.Map<LikeDTO>(l)).ToList()
                })
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
                .FirstOrDefaultAsync() ?? throw new CommentNotFoundException();

            comment.AmountOfChildComments = await _context.Comment
                .CountAsync(child => child.ParentCommentId == comment.Id);

            return comment;
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

        public async Task<List<CommentDTO>> GetCommentsByParentCommentId(int postId, int parentCommentId)
        {

            if (!await _context.Post.AnyAsync(line => line.Id == postId))
            {
                throw new PostNotFoundException();
            }

            var parentComment = await _context.Comment
                .Where(line => line.Id == parentCommentId)
                .FirstOrDefaultAsync() ?? throw new CommentNotFoundException();

            var comments = await _context.Comment
                .Where(line => line.PostId == postId && line.ParentComment == parentComment)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var childCounts = await _context.Comment
                .Where(child => child.ParentCommentId != null && child.PostId == postId)
                .GroupBy(child => child.ParentCommentId)
                .Select(group => new { ParentId = group.Key, Count = group.Count() })
                .ToListAsync();

            comments.ForEach(p => p.AmountOfChildComments = childCounts.FirstOrDefault(c => c.ParentId == p.Id)?.Count ?? 0);

            return comments;
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
        
        public async Task<LikeDTO> CreateLike(LikeDTO likeDTO)
        {
            if (await _context.Like.AnyAsync(p => p.UserId == likeDTO.UserId && p.PostId == likeDTO.PostId))
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

            return likeDTO;
        }

        public async Task<LikeDTO> GetLike(int userId, int postId)
        {
            var like = await _context.Like
                 .FirstOrDefaultAsync(p => p.UserId == userId && p.PostId == postId);

            var likeDto = _mapper.Map<LikeDTO>(like);

            return likeDto ?? throw new LikeNotFoundException();
        }

        public async Task<List<LikeDTO>> GetLikesFromPost(int postId)
        {
            var likes = await _context.Like
                .Where(like => like.PostId == postId)
                .ProjectTo<LikeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return likes;
        }

        public async Task DeleteLike(int userId, int postId)
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
