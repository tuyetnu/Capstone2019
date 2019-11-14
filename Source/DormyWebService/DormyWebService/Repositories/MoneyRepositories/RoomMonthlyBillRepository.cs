using DormyWebService.Entities;
using DormyWebService.Entities.MoneyEntities;
using DormyWebService.ViewModels.PaymentModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DormyWebService.Repositories.MoneyRepositories
{
    public class RoomMonthlyBillRepository : RepositoryBase<RoomMonthlyBill>, IRoomMonthlyBillRepository
    {
        public RoomMonthlyBillRepository(DormyDbContext context) : base(context)
        {
        }

        public RoomMonthlyBill FindPreviousBillById(int roomId, int month, int year)
        {
            return Context.RoomMonthlyBills
            .FirstOrDefault(r => r.RoomId == roomId && r.TargetMonth == month && r.TargetYear == year);
        }

        public List<RoomMonthlyBill> FindPreviousBill(int month, int year)
        {
            return Context.RoomMonthlyBills
            .Where(r => r.TargetMonth == month && r.TargetYear == year)
            .ToList();
        }

        public List<RoomAndCurrentNumber> GetRoomAndCurrentNumber(int month, int year)
        {
            return Context.RoomMonthlyBills
                .Include(r => r.Room)
                .Where(r => r.TargetMonth == month && r.TargetYear == year)
                .Select(b => new RoomAndCurrentNumber(b))
                .ToList();
        }
    }
}