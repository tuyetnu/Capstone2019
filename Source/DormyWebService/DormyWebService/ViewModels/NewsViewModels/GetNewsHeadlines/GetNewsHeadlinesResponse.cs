using System;

namespace DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines
{
    public class GetNewsHeadlinesResponse
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Status { get; set; }
    }
}