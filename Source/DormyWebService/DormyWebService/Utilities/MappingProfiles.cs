using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.ViewModels.UserModelViews;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;

namespace DormyWebService.Utilities
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UpdateStudentForm, Student>();
        }
    }
}