using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.ViewModels.RoomViewModels.ArrangeRoom;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.EditRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBookingDetail;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ImportRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.RejectRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.ResolveRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;

namespace DormyWebService.Services.TicketServices
{
    public interface IRoomBookingService
    {
        Task<RoomBookingRequestForm> FindById(int id);

        Task<SendRoomBookingResponse> SendRequest(SendRoomBookingRequest request);

        Task<bool> EditRoomRequest(EditRoomBookingRequest request);
        Task<ArrangeRoomResponseStudent> ApproveRoomBookingRequest(int id);

        Task<bool> RejectRoomBookingRequest(RejectRoomBookingRequest request);

        Task<bool> CompleteRoomBookingRequest(int id);

        Task<AdvancedGetRoomBookingResponse> AdvancedGetRoomRequest(string sorts, string filters, int? page, int? pageSize);

        Task<bool> StudentHasRoomRequestWithStatus(int studentId, List<string> statuses);

        Task<GetRoomBookingDetailResponse> GetRoomBookingDetail(int id);

        Task<bool> AutoRejectRoomBooking();

        Task<ArrangeRoomResponse> ImportRoomBookingRequests(List<ImportRoomBookingRequest> requests);
    }
}