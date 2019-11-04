namespace DormyWebService.ViewModels.RoomViewModels.GetRoomTypeInfo
{
    public class GetRoomTypeInfoResponse
    {
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int RoomTypeVacancy { get; set; }
        public decimal? RoomTypePrice { get; set; }
        public bool Gender { get; set; }
    }
}