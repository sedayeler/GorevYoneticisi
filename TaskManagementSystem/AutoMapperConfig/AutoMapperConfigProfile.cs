using AutoMapper;
using TaskManagementSystem.Models.DTOs;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<TaskCreateDto, UserTask>();
            CreateMap<UserTask, TaskGetDto>();
        }
    }
}
