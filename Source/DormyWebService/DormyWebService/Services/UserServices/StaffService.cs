using System.Net;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;

namespace DormyWebService.Services.UserServices
{
    public class StaffService : IStaffService
    {
        private IRepositoryWrapper _repoWrapper;

        public StaffService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public async Task<Staff> FindById(int id)
        {
            //Get student in database
            var staff = await _repoWrapper.Staff.FindByIdAsync(id);

            if (staff == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "StaffService: No staff is found");
            }

            return staff;
        }
    }
}