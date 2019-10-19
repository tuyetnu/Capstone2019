using DormyWebService.Repositories.ParamRepositories;
using DormyWebService.Repositories.UserRepositories;

namespace DormyWebService.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }

        IStudentRepository Student { get; }

        IParamRepository Param { get; }

        IParamTypeRepository ParamType { get;}
    }
}