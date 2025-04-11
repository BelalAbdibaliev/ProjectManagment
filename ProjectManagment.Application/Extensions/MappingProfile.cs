using AutoMapper;
using ProjectManagment.Application.Dtos;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Extensions;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateProjectDto, Project>();
        CreateMap<CreateProjectDto, Project>();
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
        CreateMap<CreateTaskDto, ProjectTask>();
        CreateMap<UpdateTaskDto, ProjectTask>();
        CreateMap<ProjectTask, TaskResponseDto>();
        CreateMap<Employee, EmployeeResponse>();
        CreateMap<Project, ProjectResponse>();
    }
}