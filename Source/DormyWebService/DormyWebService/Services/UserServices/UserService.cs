using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.UserModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DormyWebService.Services.UserServices
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repoWrapper;
        // used to get secret from appsettings
        private readonly AuthenticationSetting _authSettings;
        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=";

        public UserService(IOptions<AuthenticationSetting> authSettings, IRepositoryWrapper repoWrapper)
        {
            _authSettings = authSettings.Value;
            _repoWrapper = repoWrapper;
        }
        
        public async Task<LoginSuccessUser> Authenticate(string idToken, string email)
        {
            //TODO:Nhớ bắt phải tài khoản FPT ko

            //Call Google API with Id Token
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(GoogleApiTokenInfoUrl + idToken))
                    {
                        string test = await response.Content.ReadAsStringAsync();
                        try
                        {
                            //Covert Json to IdToken
                            IdTokenResponse idTokenResponse = JsonConvert.DeserializeObject<IdTokenResponse>(test);

                            //Log for debug
                            System.Diagnostics.Debug.WriteLine("idToken.Email: " + idTokenResponse.Email);
                            System.Diagnostics.Debug.WriteLine("Sent Email: " + email);

                            //Check if email sent and email form Google API are the same
                            if (idTokenResponse.Email != email || string.IsNullOrEmpty(idTokenResponse.Email))
                            {
                                throw new HttpStatusCodeException(404);
                            }
                        }
                        
                        catch (JsonSerializationException)
                        {
                            throw new HttpStatusCodeException(400);
                        }
                    }
                }

                try
                {
                    //Find User with the same email in database
                    var user = await _repoWrapper.User.FindAsync(u => u.Email == email);

                    //If user is null, create new user
                    if (user == null)
                    {
                        user = await _repoWrapper.User.CreateAsync(new User()
                        {
                            Email = email,
                            //Active
                            Status = UserStatus.Active,
                            Role = Role.UnAuthorizedUser,
                        });
                    }

                    // authentication successful so generate jwt token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_authSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Role, user.Role),
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    

                    return new LoginSuccessUser()
                    {
                        Role = user.Role,
                        AccessToken = tokenHandler.WriteToken(token),
                        Id = user.UserId,
                        Status = user.Status
                    };
                }
                catch (Exception)
                {
                    throw new HttpStatusCodeException(500);
                }

                
        }

        public async Task<User> ChangeStatus(int id, string status)
        {
            var user = await _repoWrapper.User.FindByIdAsync(id);

            if(user == null)
            {
                throw new HttpStatusCodeException(404);
            }
            user.Status = status;
            user = await _repoWrapper.User.UpdateAsync(user, id);

            if (user == null)
            {
                throw new HttpStatusCodeException(500);
            }

            return user;
        }

        public async Task<User> ChangeRole(int id, string role)
        {
            var user = await _repoWrapper.User.FindByIdAsync(id);

            if (user == null)
            {
                throw new HttpStatusCodeException(404); ;
            }
            user.Role = role;
            user = await _repoWrapper.User.UpdateAsync(user, id);

            if (user == null)
            {
                throw new HttpStatusCodeException(500);
            }

            return user;
        }
    }
}