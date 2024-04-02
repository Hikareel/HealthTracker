using AutoMapper;
using HealthTracker.Server.Modules.PhysicalActivity.DTOs;
using HealthTracker.Server.Modules.PhysicalActivity.Models;

namespace HealthTracker.Server.Modules.PhysicalActivity.Helpers
{
    public class GoalProfile : Profile
    {
        public GoalProfile() 
        {
            CreateMap<CreateGoalDTO, Goal>();
            CreateMap<Goal, GoalDTO>();


            CreateMap<CreateGoalTypeDTO, GoalType>();
        }
    }
}
