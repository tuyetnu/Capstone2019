using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.NewsEntities;

namespace DormyWebService.ViewModels.NewsViewModels.GetNewsDetail
{
    public class GetNewsDetailResponse
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public GetNewsDetailResponseAuthor Author { get; set; }
        public string AttachedFileUrl { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdate { get; set; }
        public string Status { get; set; }

        public static GetNewsDetailResponse CreateFromNews(News news, Admin admin)
        {
            GetNewsDetailResponseAuthor author = null;

            if (admin != null)
            {
                author = GetNewsDetailResponseAuthor.CreateFromAdmin(admin);
            }

            return new GetNewsDetailResponse()
            {
                Title = news.Title,
                Status = news.Status,
                AttachedFileUrl = news.AttachedFileUrl,
                Content = news.Content,
                CreatedDate = news.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss"),
                LastUpdate = news.LastUpdate.ToString("dd/MM/yyyy HH:mm:ss"),
                NewsId = news.NewsId,
                Author = author,
            };
        }
    }
}