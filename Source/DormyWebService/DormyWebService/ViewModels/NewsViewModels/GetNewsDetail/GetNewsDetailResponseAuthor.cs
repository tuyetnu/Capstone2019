using DormyWebService.Entities.AccountEntities;

namespace DormyWebService.ViewModels.NewsViewModels.GetNewsDetail
{
    public class GetNewsDetailResponseAuthor
    {
        public int AdminId { get; set; }
        public string Name { get; set; }

        public static GetNewsDetailResponseAuthor CreateFromAdmin(Admin admin)
        {
            return new GetNewsDetailResponseAuthor()
            {
                Name = admin.Name,
                AdminId = admin.AdminId
            };
        }
    }
}