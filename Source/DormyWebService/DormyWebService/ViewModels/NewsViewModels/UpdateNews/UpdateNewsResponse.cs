using System;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.ViewModels.NewsViewModels.CreateNews;

namespace DormyWebService.ViewModels.NewsViewModels.UpdateNews
{
    public class UpdateNewsResponse
    {
        public int NewsId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AttachedFileUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Status { get; set; }

        public static UpdateNewsResponse CreateFromNews(News news)
        {
            return new UpdateNewsResponse()
            {
                AuthorId = news.NewsId,
                NewsId = news.NewsId,
                Content = news.Content,
                AttachedFileUrl = news.AttachedFileUrl,
                LastUpdateDate = news.LastUpdate,
                CreatedDate = news.CreatedDate,
                Title = news.Title,
                Status = news.Status
            };
        }
    }
}