namespace DormyWebService.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
    }
}