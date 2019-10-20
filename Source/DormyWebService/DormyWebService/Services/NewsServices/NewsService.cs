using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.NewsViewModels;
using DormyWebService.ViewModels.NewsViewModels.CreateNews;
using DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines;

namespace DormyWebService.Services.NewsServices
{
    public class NewsService : INewsServices
    {
        private IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private IAdminService _admin;

        public NewsService(IRepositoryWrapper repoWrapper, IMapper mapper, IAdminService admin)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _admin = admin;
        }

        public async Task<List<GetNewsHeadlinesResponse>> GetNewsHeadLines()
        {
            List<News> newsList;
            try
            {
                newsList = (List<News>) await _repoWrapper.News.FindAllAsyncWithCondition(n => n.Status == NewsStatus.Active);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Failed to get news headlines");
            }

            if (!newsList.Any())
            {
                throw new HttpStatusCodeException(404, "No news headlines were found");
            }

            var result = newsList.Select(headline => _mapper.Map<GetNewsHeadlinesResponse>(headline)).ToList();

            return result;
        }

        public async Task<News> GetNewsById()
        {
            throw new NotImplementedException();
        }

        public async Task<CreateNewsResponse> CreateNews(CreateNewsRequest requestModel)
        {
            var author = await _admin.FindById(requestModel.AuthorId);
            var news = CreateNewsRequest.NewNewsFromRequest(requestModel, author);
            try
            {
                news = await _repoWrapper.News.CreateAsync(news);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Failed to create new News");
            }

            return CreateNewsResponse.CreateFromNews(news);
        }

        public async Task<News> UpdateNews(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<News> ChangeNewsStatus(int id, string newsStatus)
        {
            throw new NotImplementedException();
        }
    }
}
