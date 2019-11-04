using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.TicketEntities;

namespace DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking
{
    public class ImportStudentAndRequest
    {
        public Student Student { get; set; }
        public RoomBookingRequestForm RoomBooking { get; set; }
    }
}