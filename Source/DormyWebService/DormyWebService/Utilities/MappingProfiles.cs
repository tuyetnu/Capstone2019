using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.GetAllStudent;
using DormyWebService.ViewModels.UserModelViews.UpdateStudent;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;

namespace DormyWebService.Utilities
{
    //Declare mapping profiles
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UpdateStudentRequestForm, Student>();
            CreateMap<Student, UpdateStudentResponse>();
            CreateMap<Student, GetAllStudentResponse>();
            CreateMap<Student, FindByIdStudentResponse>();
        }
    }
}