using System.Threading.Tasks;
using DormyWebService.Repositories;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.UserServices;
using DormyWebService.ViewModels.RoomMonthlyBillViewModel.SendWaterAndElectric;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.MoneyServices
{
    public class RoomMonthlyBillService : IRoomMonthlyBillService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IStudentService _studentService;
        private readonly IParamService _paramService;
        private readonly IRoomMonthlyBillService _roomMonthlyBillService;
        private readonly IStudentMonthlyBillService _studentMonthlyBillService;

        public RoomMonthlyBillService(IRepositoryWrapper repoWrapper, IStudentService studentService, IParamService paramService, IRoomMonthlyBillService roomMonthlyBillService, IStudentMonthlyBillService studentMonthlyBillService)
        {
            _repoWrapper = repoWrapper;
            _studentService = studentService;
            _paramService = paramService;
            _roomMonthlyBillService = roomMonthlyBillService;
            _studentMonthlyBillService = studentMonthlyBillService;
        }

        public Task<ActionResult<SendNumberAndElectricNumberResponse>> SendWaterAndElectricNumber(SendNumberAndElectricNumberRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}