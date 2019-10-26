using System;
using System.Globalization;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Entities.ParamEntities;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.IssueTicketViewModels.GetIssueTicket;
using DormyWebService.ViewModels.NewsViewModels;
using DormyWebService.ViewModels.NewsViewModels.CreateNews;
using DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines;
using DormyWebService.ViewModels.Param;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
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

            //Resolve Room Booking
            CreateMap<ResolveRoomBookingRequest, RoomBookingRequestForm>();
            CreateMap<RoomBookingRequestForm, ResolveRoomBookingResponse>()
                .ForMember(dest => dest.CreatedDate, o => o.MapFrom(src => src.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat)))
                .ForMember(dest => dest.LastUpdated, o => o.MapFrom(src => src.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat)));

            //Get Room Booking
            CreateMap<RoomBookingRequestForm, GetRoomBookingResponse>()
                .ForMember(dest => dest.CreatedDate, o => o.MapFrom(src => src.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat)))
                .ForMember(dest => dest.LastUpdated, o => o.MapFrom(src => src.LastUpdated.ToString(GlobalParams.DateTimeResponseFormat)));

            //GetEquipment
            CreateMap<Equipment, GetEquipmentResponse>();
        }
    }
}