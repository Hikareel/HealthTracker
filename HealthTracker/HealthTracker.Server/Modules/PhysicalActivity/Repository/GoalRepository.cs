using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.Exceptions.PhysicalActivity;
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
        Task<GoalTypeDTO> CreateGoalType(CreateGoalTypeDTO createGoalTypeDTO);
        Task<GoalTypeDTO> GetGoalType(int id);
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
                throw new Exception("User Not Found"); //Zamienić później na UserNotFoundException()
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
    }
}
