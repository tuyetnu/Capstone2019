using System;
using System.Globalization;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.ViewModels.NewsViewModels;
using DormyWebService.ViewModels.NewsViewModels.CreateNews;
using DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines;
using DormyWebService.ViewModels.Param;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.GetAllStudent;
using DormyWebService.ViewModels.UserModelViews.GetUser;
using DormyWebService.ViewModels.UserModelViews.UpdateStudent;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;

namespace DormyWebService.Utilities
{
    //Declare mapping profiles
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UpdateStudentRequest, Student>();
            CreateMap<CreateNewsRequest, News>();
            CreateMap<Student, UpdateStudentResponse>();
            CreateMap<Student, GetAllStudentResponse>()
                .ForMember(dest => dest.BirthDay, source => source.MapFrom(src => src.BirthDay.ToString(GlobalParams.BirthDayFormat)));
            CreateMap<Student, FindByIdStudentResponse>();
            CreateMap<User, GetUserResponse>();
            CreateMap<News, CreateNewsResponse>();
            CreateMap<News, GetNewsHeadlinesResponse>();
            CreateMap<Param, ParamModelView>();
        }
    }
}