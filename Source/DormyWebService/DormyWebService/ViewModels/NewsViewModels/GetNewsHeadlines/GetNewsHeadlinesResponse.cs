using System;
using DormyWebService.Entities.NewsEntities;

namespace DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines
{
    public class GetNewsHeadlinesResponse
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdate { get; set; }
        public string Status { get; set; }

        public static GetNewsHeadlinesResponse CreateFromNews(News news)
        {

            return new GetNewsHeadlinesResponse()
            {
                Title = news.Title,
                Status = news.Status,
                Content = news.Content,
                CreatedDate = news.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss"),
                LastUpdate = news.LastUpdate.ToString("dd/MM/yyyy HH:mm:ss"),
                NewsId = news.NewsId,
            };
        }
    }
}