using DormyWebService.ViewModels.PaymentModels;
using DormyWebService.ViewModels.RoomMonthlyBillViewModel.SendWaterAndElectric;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DormyWebService.Services.MoneyServices
{
    public interface IRoomMonthlyBillService
    {
        Task SendWaterAndElectricNumber(SendNumberAndElectricNumberRequest request);
        Task GenerateStudentBill();
        Task<List<RoomAndCurrentNumber>> GetRoomAndPreviousNumber();
        StudentBillResponse GetStudentBill(StudentBillRequest studentBillRequest);
    }
}