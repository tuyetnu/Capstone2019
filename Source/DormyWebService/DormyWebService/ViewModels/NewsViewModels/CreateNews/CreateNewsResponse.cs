using System;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Utilities;

namespace DormyWebService.ViewModels.NewsViewModels.CreateNews
{
    public class CreateNewsResponse
    {
        public int NewsId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AttachedFileUrl { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdateDate { get; set; }
        public string Status { get; set; }

        public static CreateNewsResponse CreateFromNews(News news)
        {
            return new CreateNewsResponse()
            {
                AuthorId = news.NewsId,
                NewsId = news.NewsId,
                Content = news.Content,
                AttachedFileUrl = news.AttachedFileUrl,
                LastUpdateDate = news.LastUpdate.ToString(GlobalParams.DateTimeResponseFormat),
                CreatedDate = news.CreatedDate.ToString(GlobalParams.DateTimeResponseFormat),
                Title = news.Title,
                Status = news.Status
            };
        }
    }
}