namespace DormyWebService.ViewModels.HomeModelView
{
    public class HomeResponse
    {
        public int NumberOfUnseenNotification { get; set; }
        public bool IsHaveRoom { get; set; }
        public bool IsHaveRequestBooking { get; set; }
        public bool IsHaveRequestTransfer { get; set; }
        public bool IsHaveRequestCancel { get; set; }
        public bool IsHaveRequestRenew { get; set; }
        public bool IsHavePayment { get; set; }

        public static HomeResponse NewResponse(int numberOfUnseenNotification, 
            bool isHaveRoom, bool isHaveRequestBooking, bool isHaveRequestTransfer, bool isHaveRequestCancel, bool isHaveRequestRenew, bool isHavePayment)
        {
            return new HomeResponse()
            {
                IsHavePayment = isHavePayment,
                IsHaveRequestBooking = isHaveRequestBooking,
                IsHaveRequestCancel = isHaveRequestCancel,
                IsHaveRequestRenew = isHaveRequestRenew,
                IsHaveRequestTransfer = isHaveRequestTransfer,
                IsHaveRoom = isHaveRoom,
                NumberOfUnseenNotification = numberOfUnseenNotification,
            };
        }
    }
}