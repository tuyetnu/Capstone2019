using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.NewsEntities;

namespace DormyWebService.ViewModels.NewsViewModels.UpdateNews
{
    public class UpdateNewsRequest
    {
        [Required]
        public int NewsId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string AttachedFileUrl { get; set; }
        [Required]
        public string Status { get; set; }

        public static News UpdateToNews(UpdateNewsRequest request, News news)
        {
            news.Title = request.Title;
            news.Content = request.Content;
            news.AttachedFileUrl = request.AttachedFileUrl;
            news.Status = request.Status;
            news.LastUpdate = DateTime.Now.AddHours(7);

            return news;
        }
    }
}