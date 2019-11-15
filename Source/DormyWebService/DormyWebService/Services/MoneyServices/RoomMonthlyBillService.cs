using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.MoneyEntities;
using DormyWebService.Entities.RoomEntities;
using DormyWebService.Repositories;
using DormyWebService.ViewModels.PaymentModels;
using DormyWebService.ViewModels.RoomMonthlyBillViewModel.SendWaterAndElectric;

namespace DormyWebService.Services.MoneyServices
{
    public class RoomMonthlyBillService : IRoomMonthlyBillService
    {
        private readonly IRepositoryWrapper _repoWrapper;

        public RoomMonthlyBillService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public async Task GenerateStudentBill()
        {
            List<Student> find = _repoWrapper.Student.GetAllStudentInDormitoryIncluding();
            if (find.Count == 0)
            {
                throw new Exception("Không có sinh viên");
            }
            var nextMonth = DateTime.Now.AddMonths(1);
            var monthOfNextMonth = nextMonth.Month;
            var yearOfNextMonth = nextMonth.Year;
            List<RoomMonthlyBill> currentRoomBills = _repoWrapper.RoomMonthlyBill.FindPreviousBill(monthOfNextMonth, yearOfNextMonth);
            if (currentRoomBills == null || currentRoomBills.Count == 0)
            {
                throw new Exception("Chưa ghi điện nước tháng này");
            }
            List<StudentMonthlyBill> result = new List<StudentMonthlyBill>();
            foreach (Student student in find)
            {
                var roomMonthlyBillId = currentRoomBills.FirstOrDefault(r => r.RoomId == student.Room.RoomId).RoomMonthlyBillId;
                var total = currentRoomBills.FirstOrDefault(r => r.RoomId == student.Room.RoomId).TotalAmount;
                StudentMonthlyBill studentMonthlyBill = new StudentMonthlyBill
                {
                    IsPaid = false,
                    StudentId = student.StudentId,
                    RoomId = student.Room.RoomId,
                    RoomMonthlyBillId = roomMonthlyBillId,
                    Total = total,
                    TargetMonth = monthOfNextMonth,
                    TargetYear = yearOfNextMonth,
                    PaidDate = DateTime.Now
                };
                result.Add(_repoWrapper.StudentMonthlyBill.CreateWithoutSave(studentMonthlyBill));
            }
            await _repoWrapper.Save();
        }

        public async Task<List<RoomAndCurrentNumber>> GetRoomAndPreviousNumber()
        {
            var now = DateTime.Now;
            var month = now.Month;
            var year = now.Year;
            List<RoomAndCurrentNumber> result = _repoWrapper.RoomMonthlyBill.GetRoomAndCurrentNumber(month, year);
            if (result == null) result = new List<RoomAndCurrentNumber>();
            List<int> roomIds = new List<int>();
            foreach(var room in result)
            {
                roomIds.Add(room.RoomId);
            }
            List<Room> rooms = (List<Room>)await _repoWrapper.Room.FindAllAsyncWithCondition(r => !roomIds.Contains(r.RoomId) && r.RoomStatus == "Active");
            foreach (var room in rooms)
            {
                RoomAndCurrentNumber roomAndCurrentNumber = new RoomAndCurrentNumber
                {
                    CurrentElectricityNumber = 0,
                    CurrentWaterNumber = 0,
                    RoomId = room.RoomId,
                    HasNewNumber = false,
                    RoomName = room.Name
                };
                result.Add(roomAndCurrentNumber);
            }
            var nextMonth = now.AddMonths(1);
            var monthOfNextMonth = nextMonth.Month;
            var yearOfNextMonth = nextMonth.Year;
            List<RoomAndCurrentNumber> newBills = _repoWrapper.RoomMonthlyBill.GetRoomAndCurrentNumber(monthOfNextMonth, yearOfNextMonth);
            foreach (RoomAndCurrentNumber newBill in newBills)
            {
                var tmp = result.FirstOrDefault(r => r.RoomId == newBill.RoomId);
                if (tmp != null)
                {
                    tmp.HasNewNumber = true;
                }
            }
            return result;
        }

        public StudentBillResponse GetStudentBill(StudentBillRequest studentBillRequest)
        {
            return _repoWrapper.StudentMonthlyBill.GetBillByRequest(studentBillRequest);
        }

        public async Task SendWaterAndElectricNumber(SendNumberAndElectricNumberRequest request)
        {
            var now = DateTime.Now;
            var month = now.Month;
            var year = now.Year;
            var nextMonth = now.AddMonths(1);
            var monthOfNextMonth = nextMonth.Month;
            var yearOfNextMonth = nextMonth.Year;

            RoomMonthlyBill check = _repoWrapper.RoomMonthlyBill.FindPreviousBillById(request.RoomId, monthOfNextMonth, yearOfNextMonth);
            if (check != null)
            {
                throw new Exception("Đã nhập điện nước tháng này");
            }

            RoomMonthlyBill previousBill = _repoWrapper.RoomMonthlyBill.FindPreviousBillById(request.RoomId, month, year);

            var previousWaterNumber = 0;
            var previousElectricityNumber = 0;
            if (previousBill != null)
            {
                previousWaterNumber = previousBill.PreviousWaterNumber;
                previousElectricityNumber = previousBill.PreviousElectricityNumber;
            }
            PricePerUnit pricePerWater = _repoWrapper.PricePerUnit.FindOneById(29);
            PricePerUnit pricePerElectricity = _repoWrapper.PricePerUnit.FindOneById(30);
            Room room = _repoWrapper.Room.FindOneById(request.RoomId);
            PricePerUnit pricePerRoom = null;
            if (room.RoomType == 11)
            {
                pricePerRoom = _repoWrapper.PricePerUnit.FindOneById(31);
            }
            else
            {
                pricePerRoom = _repoWrapper.PricePerUnit.FindOneById(32);
            }

            var waterBill = (request.WaterNumber - previousWaterNumber) * pricePerWater.Price;
            var electricityBill = (request.ElectricNumber - previousElectricityNumber) * pricePerElectricity.Price;
            var roomBill = pricePerRoom.Price;

            RoomMonthlyBill currentBill = new RoomMonthlyBill
            {
                RoomId = request.RoomId,
                CreatedDate = now,
                LastUpdated = now,
                PreviousWaterNumber = previousWaterNumber,
                NewWaterNumber = request.WaterNumber,
                PricePerWaterId = pricePerWater.PricePerUnitId,
                PreviousElectricityNumber = previousElectricityNumber,
                NewElectricityNumber = request.ElectricNumber,
                PricePerElectricityId = pricePerElectricity.PricePerUnitId,
                PricePerRoomId = pricePerRoom.PricePerUnitId,
                TargetMonth = monthOfNextMonth,
                TargetYear = yearOfNextMonth,
                WaterBill = waterBill,
                ElectricityBill = electricityBill,
                RoomBill = roomBill,
                TotalAmount = waterBill + electricityBill + roomBill
            };
            _repoWrapper.RoomMonthlyBill.CreateWithoutSave(currentBill);
            await _repoWrapper.Save();
        }
    }
}