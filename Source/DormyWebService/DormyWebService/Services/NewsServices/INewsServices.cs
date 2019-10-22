using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.ViewModels.NewsViewModels;
using DormyWebService.ViewModels.NewsViewModels.CreateNews;
using DormyWebService.ViewModels.NewsViewModels.GetNewsDetail;
using DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines;
using DormyWebService.ViewModels.NewsViewModels.UpdateNews;

namespace DormyWebService.Services.NewsServices
{
    public interface INewsServices
    {
        Task<List<GetNewsHeadlinesResponse>> GetActiveNewsHeadLines();
        Task<List<GetNewsHeadlinesResponse>> GetNewsHeadLines();
        Task<List<GetNewsHeadlinesResponse>> AdvancedGetNewsHeadLines(string sorts, string filters, int? page, int? pageSize);
        Task<News> FindById(int id);
        Task<GetNewsDetailResponse> GetNewsDetail(int id);
        Task<CreateNewsResponse> CreateNews(CreateNewsRequest requestModel);
        Task<UpdateNewsResponse> UpdateNews(UpdateNewsRequest requestModel);
        Task<UpdateNewsResponse> ChangeNewsStatus(int id, string newsStatus);

    }
}