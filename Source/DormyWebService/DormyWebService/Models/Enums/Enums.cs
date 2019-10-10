namespace DormyWebService.Models.Enums
{
    public enum StudentStatusEnum
    {
        Active,
        Disabled,
    }

    public enum RequestStatusEnum
    {
        Pending,
        Rejected,
        Completed,
    }

    public enum ContractStatusEnum
    {
        Pending,
        Active,
        Expired,
    }

    public enum RoomStatusEnum
    {
        Active,
        Disabled,
    }

    public enum IssueStatusEnum
    {
        Pending,
        Rejected,
        Resolved,
    }

    public enum NotificationStatusEnum
    {
        Unseen,
        Seen
    }
}