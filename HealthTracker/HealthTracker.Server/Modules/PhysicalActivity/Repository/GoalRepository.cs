using AutoMapper;
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
        Task<GoalType> CreateGoalType(CreateGoalTypeDTO createGoalTypeDTO);
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

        public async Task<GoalType> CreateGoalType(CreateGoalTypeDTO createGoalTypeDTO)
        {
            var result = _mapper.Map<GoalType>(createGoalTypeDTO);
            await _context.GoalType.AddAsync(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
