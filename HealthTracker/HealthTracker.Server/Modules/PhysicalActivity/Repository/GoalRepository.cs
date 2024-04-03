using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Core.Exceptions.PhysicalActivity;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.PhysicalActivity.DTOs;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.PhysicalActivity.Repository
{
    public interface IGoalRepository
    {
        Task<GoalDTO> CreateGoal(CreateGoalDTO createGoalDTO);
        Task<GoalDTO> GetGoal(int id);
        Task<List<GoalDTO>> GetUsersGoals(int userId);
        Task<GoalDTO> ChangeGoalStatus(ChangeGoalDTO changeGoalDTO);
        Task DeleteGoal(int goalId);
        Task<GoalTypeDTO> CreateGoalType(CreateGoalTypeDTO createGoalTypeDTO);
        Task<GoalTypeDTO> GetGoalType(int id);
        Task<List<GoalTypeDTO>> GetGoalTypes(int pageNumber, int pageSize);
    }
    public class GoalRepository : IGoalRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GoalRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GoalDTO> CreateGoal(CreateGoalDTO createGoalDTO)
        {
            var user = await _context.User.AnyAsync(line => line.Id == createGoalDTO.UserId);
            var goaltype = await _context.GoalType.AnyAsync(line => line.Id == createGoalDTO.GoalTypeId);

            if(!user)
            {
                throw new UserNotFoundException();
            }
            if (!goaltype)
            {
                throw new GoalTypeNotFoundException();
            }

            var result = _mapper.Map<Goal>(createGoalDTO);
            result.IsDone = false;
            
            await _context.Goal.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<GoalDTO>(result);
        }

        public async Task<GoalDTO> GetGoal(int id)
        {
            var goal = await _context.Goal
                .Where(line => line.Id == id)
                .ProjectTo<GoalDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return goal ?? throw new GoalNotFoundException();
        }

        public async Task<List<GoalDTO>> GetUsersGoals(int userId)
        {
            var user = await _context.User.AnyAsync(line => line.Id == userId);

            if (!user)
            {
                throw new UserNotFoundException(userId);
            }

            var goals = await _context.Goal
                .Where(line => line.UserId == userId)
                .ProjectTo<GoalDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return goals;
        }

        public async Task<GoalDTO> ChangeGoalStatus(ChangeGoalDTO changeGoalDTO)
        {
            var goal = await _context.Goal.FindAsync(changeGoalDTO.Id);
            
            if (goal == null)
            {
                throw new GoalNotFoundException();
            }

            goal.GoalTypeId = changeGoalDTO.GoalTypeId ?? goal.GoalTypeId;
            goal.Content = changeGoalDTO.Content ?? goal.Content;
            goal.DateOfEnd = changeGoalDTO.DateOfEnd ?? goal.DateOfEnd;
            goal.IsDone = changeGoalDTO.IsDone ?? goal.IsDone;

            await _context.SaveChangesAsync();        

            return _mapper.Map<GoalDTO>(goal);
        }

        public async Task DeleteGoal(int goalId)
        {
            var goal = await _context.Goal
                .Where(f => f.Id == goalId)
                .FirstOrDefaultAsync();

            if (goal == null)
            {
                throw new GoalNotFoundException();
            }

            _context.Goal.Remove(goal);
            await _context.SaveChangesAsync();
        }


        public async Task<GoalTypeDTO> CreateGoalType(CreateGoalTypeDTO createGoalTypeDTO)
        {
            var result = _mapper.Map<GoalType>(createGoalTypeDTO);
            await _context.GoalType.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<GoalTypeDTO>(result);
        }

        public async Task<GoalTypeDTO> GetGoalType(int id)
        {
            var goaltype = await _context.GoalType
                .Where(line => line.Id == id)
                .ProjectTo<GoalTypeDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return goaltype ?? throw new GoalTypeNotFoundException();
        }

        public async Task<List<GoalTypeDTO>> GetGoalTypes(int pageNumber, int pageSize)
        {
            var goaltypes = await _context.GoalType
                .ProjectTo<GoalTypeDTO>(_mapper.ConfigurationProvider)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return goaltypes;
        }
    }
}
