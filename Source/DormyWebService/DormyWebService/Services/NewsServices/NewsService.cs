using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.NewsViewModels;
using DormyWebService.ViewModels.NewsViewModels.CreateNews;
using DormyWebService.ViewModels.NewsViewModels.GetNewsDetail;
using DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines;
using DormyWebService.ViewModels.NewsViewModels.UpdateNews;
using Microsoft.EntityFrameworkCore.Internal;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.NewsServices
{
    public class NewsService : INewsServices
    {
        private IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private IAdminService _admin;
        private ISieveProcessor _sieveProcessor;

        public NewsService(IRepositoryWrapper repoWrapper, IMapper mapper, IAdminService admin, ISieveProcessor sieveProcessor)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _admin = admin;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<List<GetNewsHeadlinesResponse>> GetActiveNewsHeadLines()
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

            return newsList.Select(GetNewsHeadlinesResponse.CreateFromNews).ToList();
        }

        public async Task<List<GetNewsHeadlinesResponse>> GetNewsHeadLines()
        {
            List<News> newsList;
            try
            {
                newsList = (List<News>)await _repoWrapper.News.FindAllAsync();
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Failed to get news headlines");
            }

            if (!newsList.Any())
            {
                throw new HttpStatusCodeException(404, "No news headlines were found");
            }

            return newsList.Select(GetNewsHeadlinesResponse.CreateFromNews).ToList();
        }

        public async Task<List<GetNewsHeadlinesResponse>> AdvancedGetNewsHeadLines(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            ICollection<News> newsHeadLines;

            try
            {
                newsHeadLines = await _repoWrapper.News.FindAllAsync();
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "RoomService: Internal Server Error Occured Searching for room");
            }

            if (newsHeadLines == null || newsHeadLines.Any() == false)
            {
                throw new HttpStatusCodeException(404, "RoomService: No room is found");
            }

            var result = _sieveProcessor.Apply(sieveModel, newsHeadLines.AsQueryable()).ToList();
            return result.Select(GetNewsHeadlinesResponse.CreateFromNews).ToList(); ;
        }

        public async Task<News> FindById(int id)
        {
            News news;
            try
            {
                news = await _repoWrapper.News.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Internal Server Error Occured when finding news");
            }

            //Check if there's this user in database
            if (news == null)
            {
                throw new HttpStatusCodeException(404, "News is not found");
            }

            return news;
        }

        public async Task<GetNewsDetailResponse> GetNewsDetail(int id)
        {
            var news = await FindById(id);
            var author = await _admin.FindById(news.AuthorId);
            return GetNewsDetailResponse.CreateFromNews(news, author);
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
                throw new HttpStatusCodeException(500, "Internal Server Error. Failed to create new News");
            }

            return CreateNewsResponse.CreateFromNews(news);
        }

        public async Task<UpdateNewsResponse> UpdateNews(UpdateNewsRequest requestModel)
        {
            var news = await FindById(requestModel.NewsId);

            news = UpdateNewsRequest.UpdateToNews(requestModel, news);

            try
            {
                news = await _repoWrapper.News.UpdateAsync(news, news.NewsId);
            }
            catch (Exception )
            {
                throw new HttpStatusCodeException(500, "Internal Server Error. Failed to update News to Database");
            }

            return UpdateNewsResponse.CreateFromNews(news);
        }

        public async Task<UpdateNewsResponse> ChangeNewsStatus(int id, string newsStatus)
        {
            var news = await FindById(id);
            news.Status = newsStatus;

            try
            {
                news = await _repoWrapper.News.UpdateAsync(news, news.NewsId);
            }
            catch (Exception)
            {
                throw new HttpStatusCodeException(500, "Internal Server Error. Failed to update News to Database");
            }

            return UpdateNewsResponse.CreateFromNews(news);
        }
    }
}
