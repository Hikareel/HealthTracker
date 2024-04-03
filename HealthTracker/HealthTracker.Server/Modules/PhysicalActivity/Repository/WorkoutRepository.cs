using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Core.Exceptions.PhysicalActivity;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.PhysicalActivity.DTOs;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.PhysicalActivity.Repository
{
    public interface IWorkoutRepository
    {
        Task<WorkoutDTO> CreateWorkout(CreateWorkoutDTO createWorkoutDTO);
        Task<WorkoutDTO> GetWorkout(int id);
    }
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WorkoutRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WorkoutDTO> CreateWorkout(CreateWorkoutDTO createWorkoutDTO)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(line => line.Id == createWorkoutDTO.UserId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var result = _mapper.Map<Workout>(createWorkoutDTO);

            await _context.Workout.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<WorkoutDTO>(result);
        }

        public async Task<WorkoutDTO> GetWorkout(int id)
        {
            var workout = await _context.Workout
                .ProjectTo<WorkoutDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(line => line.Id == id);

            return workout ?? throw new WorkoutNotFoundException();
        }
    }
}
