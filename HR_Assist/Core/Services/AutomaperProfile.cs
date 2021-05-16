using AutoMapper;
using HR_Assist.Core.Entities;

using HR_Assist.Core.Services.Projects;

using System.Linq;

namespace ScrumBase.Core.Services
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
