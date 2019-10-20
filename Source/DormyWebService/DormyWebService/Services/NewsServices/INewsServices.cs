using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.ViewModels.NewsViewModels;

namespace DormyWebService.Services.NewsServices
{
    public interface INewsServices
    {
        Task<List<News>> FindAllNewsHeadLines();
        Task<News> GetNewsById();
        Task<CreateNewsResponse> CreateNews(CreateNewsRequest requestModel);
        Task<News> UpdateNews(int id);
        Task<News> ChangeNewsStatus(int id, string newsStatus);

    }
}