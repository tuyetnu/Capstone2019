using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.ChangeStudentStatus;
using DormyWebService.ViewModels.UserModelViews.CheckStudentForRenewContract;
using DormyWebService.ViewModels.UserModelViews.GetAllStudent;
using DormyWebService.ViewModels.UserModelViews.GetStudentProfile;
using DormyWebService.ViewModels.UserModelViews.ImportStudent;
using DormyWebService.ViewModels.UserModelViews.UpdateStudent;

namespace DormyWebService.Services.UserServices
{
    public interface IStudentService
    {
        Task<List<GetAllStudentResponse>> GetAllStudent();
        Task<List<GetAllStudentResponse>> AdvancedGetStudent(string sorts, string filters, int? page, int? pageSize);
        Task<CheckStudentForRenewContractResponse> CheckStudentForRenewContract(int id);
        Task<Student> FindById(int id);
        Task<GetStudentProfileResponse> GetProfile(int id);
        Task<List<ImportStudentResponse>> ImportStudent(List<ImportStudentRequest> requestModel);
        Task<UpdateStudentResponse> UpdateStudent(UpdateStudentRequest requestModel);
        Task<ChangeStudentStatusResponse> ChangeStudentStatus(int id, string status);
        Task<bool> HasARoom(int studentId);
        Task<bool> AutoResetEvaluationPoint();
        Task<bool> ResetEvaluationPoint();
    }
}