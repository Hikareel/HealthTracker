using AutoMapper;
using HealthTracker.Server.Modules.PhysicalActivity.DTOs;
using HealthTracker.Server.Modules.PhysicalActivity.Models;

namespace HealthTracker.Server.Modules.PhysicalActivity.Helpers
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile() 
        {
            CreateMap<CreateWorkoutDTO, Workout>();
            CreateMap<Workout, WorkoutDTO>();
        }
    }
}
