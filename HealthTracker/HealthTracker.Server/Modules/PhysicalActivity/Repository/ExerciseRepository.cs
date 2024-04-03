using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Core.Exceptions.PhysicalActivity;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.PhysicalActivity.DTOs;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace HealthTracker.Server.Modules.PhysicalActivity.Repository
{
    public interface IExerciseRepository
    {
        Task<ExerciseDTO> CreateExercise(CreateExerciseDTO createExerciseDTO);
        Task<ExerciseDTO> GetExercise(int id);
        Task DeleteExercise(int id);
        Task<ExerciseTypeDTO> CreateExerciseType(CreateExerciseTypeDTO createExerciseTypeDTO);
        Task<ExerciseTypeDTO> GetExerciseType(int id);
        Task DeleteExerciseType(int id);
    }
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExerciseRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExerciseDTO> CreateExercise(CreateExerciseDTO createExerciseDTO)
        {
            var exerciseType = await _context.ExerciseType
                .FirstOrDefaultAsync(line => line.Id == createExerciseDTO.ExerciseTypeId);

            if (exerciseType == null)
            {
                throw new ExerciseTypeNotFoundException();
            }

            var result = _mapper.Map<Exercise>(createExerciseDTO);

            await _context.Exercise.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<ExerciseDTO>(result);
        }

        public async Task<ExerciseDTO> GetExercise(int id)
        {
            var exercise = await _context.Exercise
                .ProjectTo<ExerciseDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(line => line.Id == id);

            return exercise ?? throw new ExerciseNotFoundException();
        }


        public async Task<ExerciseTypeDTO> CreateExerciseType(CreateExerciseTypeDTO createExerciseTypeDTO)
        {
            var result = _mapper.Map<ExerciseType>(createExerciseTypeDTO);

            await _context.ExerciseType.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<ExerciseTypeDTO>(result);
        }

        public async Task<ExerciseTypeDTO> GetExerciseType(int id)
        {
            var result = await _context.ExerciseType
                .ProjectTo<ExerciseTypeDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(line => line.Id == id);

            return result ?? throw new ExerciseTypeNotFoundException();
        }

        public async Task DeleteExercise(int id)
        {
            var exercise = await _context.Exercise
                .FirstOrDefaultAsync(line => line.Id == id);

            if (exercise == null)
            {
                throw new ExerciseNotFoundException();
            }

            _context.Exercise.Remove(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExerciseType(int id)
        {
            var exercisetype = await _context.ExerciseType
                .FirstOrDefaultAsync(line => line.Id == id);

            if (exercisetype == null)
            {
                throw new ExerciseTypeNotFoundException();
            }

            _context.ExerciseType.Remove(exercisetype);
            await _context.SaveChangesAsync();
        }
    }
}
