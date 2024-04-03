using AutoMapper;
using HealthTracker.Server.Modules.PhysicalActivity.DTOs;
using HealthTracker.Server.Modules.PhysicalActivity.Models;

namespace HealthTracker.Server.Modules.PhysicalActivity.Helpers
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile() 
        {
            CreateMap<CreateExerciseDTO, Exercise>();
            CreateMap<Exercise, ExerciseDTO>();

            CreateMap<CreateExerciseTypeDTO, ExerciseType>();
            CreateMap<ExerciseType, ExerciseTypeDTO>();
        }
    }
}
