using System.Threading.Tasks;
using DormyWebService.ViewModels.RoomMonthlyBillViewModel.SendWaterAndElectric;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Services.MoneyServices
{
    public interface IRoomMonthlyBillService
    {
        Task<ActionResult<SendNumberAndElectricNumberResponse>> SendWaterAndElectricNumber(SendNumberAndElectricNumberRequest request);
    }
}