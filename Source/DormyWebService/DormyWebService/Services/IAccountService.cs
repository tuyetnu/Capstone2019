using System.Collections.Generic;
using DormyWebService.Entities;
using DormyWebService.Entities.Account;

namespace DormyWebService.Services
{
    public interface IAccountService
    {
            Account Authenticate(string username, string password);
            IEnumerable<Account> GetAll();
            Account GetById(int id);
    }
}