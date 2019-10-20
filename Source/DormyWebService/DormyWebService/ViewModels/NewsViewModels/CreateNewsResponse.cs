using System;
using DormyWebService.Entities.NewsEntities;

namespace DormyWebService.ViewModels.NewsViewModels
{
    public class CreateNewsResponse
    {
        public int NewsId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AttachedFileUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Status { get; set; }

        public static CreateNewsResponse CreateFromNews(News news)
        {
            return new CreateNewsResponse()
            {
                AuthorId = news.NewsId,
                NewsId = news.NewsId,
                Content = news.Content,
                AttachedFileUrl = news.AttachedFileUrl,
                LastUpdateDate = news.CreatedDate,
                CreatedDate = news.CreatedDate,
                Title = news.Title,
                Status = news.Status
            };
        }
    }
}