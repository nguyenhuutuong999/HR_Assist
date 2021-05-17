using AutoMapper;
using HR.Assist.Core.Entities;

using HR.Assist.Core.Services.Projects;

using System.Linq;

namespace HR.Assist.Core.Services
{

    //public class UserProfile : Profile
    //{
    //    public UserProfile()
    //    {
    //        CreateMap<ApplicationUser, UserDTO>();
    //        CreateMap<ApplicationUser, UserInStoryDTO>();

    //        CreateMap<UserCreateRequest, ApplicationUser>();
    //        CreateMap<UserEditRequest, ApplicationUser>();
    //    }
    //}

   
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectCreateRequest, Project>();
            CreateMap<ProjectEditRequest, Project>();
        }
    }

   
}
