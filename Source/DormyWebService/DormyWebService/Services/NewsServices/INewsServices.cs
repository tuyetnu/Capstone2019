using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.ViewModels.NewsViewModels;
using DormyWebService.ViewModels.NewsViewModels.CreateNews;
using DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines;

namespace DormyWebService.Services.NewsServices
{
    public interface INewsServices
    {
        Task<List<GetNewsHeadlinesResponse>> GetNewsHeadLines();
        Task<News> GetNewsById();
        Task<CreateNewsResponse> CreateNews(CreateNewsRequest requestModel);
        Task<News> UpdateNews(int id);
        Task<News> ChangeNewsStatus(int id, string newsStatus);

    }
}