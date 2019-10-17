using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.AccountModelViews;
using Microsoft.Extensions.Options;

namespace DormyWebService.Services
{
    public class UserService : IUserService
    {
        // use to get secret from appsettings
        private readonly AuthenticationSetting _authSettings;

        public UserService(IOptions<AuthenticationSetting> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public Task<LoginSuccessUser> Authenticate(SocialUser socialUser)
        {
//            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
//            if (user == null)
//                return null;

            // authentication successful so generate jwt token
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.Name, user.Id.ToString())
//                }),
//                Expires = DateTime.UtcNow.AddDays(7),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            user.Token = tokenHandler.WriteToken(token);

//            return user;

            throw new System.NotImplementedException();
        }

        public Task<List<User>> FindAll()
        {
            throw new System.NotImplementedException();
        }
    }
}