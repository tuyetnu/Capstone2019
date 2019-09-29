using System.Collections.Generic;
using DormyWebService.Entities;

namespace DormyWebService.Services
{
    public interface IAccountService
    {
            Account Authenticate(string username, string password);
            IEnumerable<Account> GetAll();
            Account GetById(int id);
    }
}