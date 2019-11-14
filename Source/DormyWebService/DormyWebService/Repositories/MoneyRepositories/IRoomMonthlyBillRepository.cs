using DormyWebService.Entities.MoneyEntities;
using DormyWebService.ViewModels.PaymentModels;
using System.Collections.Generic;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public interface IRoomMonthlyBillRepository : IRepository<RoomMonthlyBill>
    {
        RoomMonthlyBill FindPreviousBillById(int roomId, int month, int year);
        List<RoomMonthlyBill> FindPreviousBill(int month, int year);
        List<RoomAndCurrentNumber> GetRoomAndCurrentNumber(int month, int year);
    }
}