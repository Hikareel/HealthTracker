﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            try
            {
                var post = _mapper.Map<Post>(postDTO);

                await _context.Post.AddAsync(post);
                await _context.SaveChangesAsync();

                return _mapper.Map<PostDTO>(post);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<PostDTO> GetPost(int postId)
        {
            var post = await _context.Post
                .FirstOrDefaultAsync(p => p.Id == postId);

            if(post == null)
            {
                return null;
            }

            var postDto = _mapper.Map<PostDTO>(post);

            return postDto;
        }

        public async Task<PostListDTO> GetPosts(int userId, int pageSize, int pageNumber)
        {
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

            return new PostListDTO
            {
                UserId = userId,
                Posts = posts
            };
        }

        public async Task<CommentDTO> CreateComment(int? parentCommentId, CreateCommentDTO commentDTO)
        {
            try
            {
                var comment = _mapper.Map<Comment>(commentDTO);
                comment.ParentCommentId = parentCommentId;

                await _context.Comment.AddAsync(comment);
                await _context.SaveChangesAsync();

                return _mapper.Map<CommentDTO>(comment);
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
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return comment;
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
