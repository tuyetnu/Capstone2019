using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.AccountModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DormyWebService.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repoWrapper;
        // use to get secret from appsettings
        private readonly AuthenticationSetting _authSettings;
        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=";

        public UserService(IOptions<AuthenticationSetting> authSettings, IRepositoryWrapper repoWrapper)
        {
            _authSettings = authSettings.Value;
            _repoWrapper = repoWrapper;
        }
        
        public async Task<ActionResult<LoginSuccessUser>> Authenticate(SocialUser socialUser)
        {
            IdToken idToken;

            //Call Google API with Id Token
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(GoogleApiTokenInfoUrl + socialUser.IdToken))
                    {
                        string test = await response.Content.ReadAsStringAsync();
                        try
                        {
                            //Covert Json to IdToken
                            idToken = JsonConvert.DeserializeObject<IdToken>(test);
                        }
                        
                        catch (JsonSerializationException)
                        {
                            return new StatusCodeResult(StatusCodes.Status400BadRequest);
                        }
                    }
                }

                if (idToken.Email != socialUser.Email || string.IsNullOrEmpty(idToken.Email))
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            //Find User with the same email in database
                var user = await _repoWrapper.User.FindAsync(u => u.Email == socialUser.Email);

                //If user is null, create new user
                if (user == null)
                {
                    user = await _repoWrapper.User.CreateAsync(new User()
                    {
                        Email = idToken.Email,
                        //Active
                        Status = UserStatus.Active,
                    });
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_authSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Id", user.UserId.ToString()), 
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, Role.Admin),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.AccessToken = tokenHandler.WriteToken(token);

                return new LoginSuccessUser()
                {
                    AccessToken = user.AccessToken
                };
        }

        public async Task<ActionResult<User>> ChangeStatus(int id, string status)
        {
            var user = await _repoWrapper.User.FindByIdAsync(id);

            if(user == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            user.Status = status;
            user = await _repoWrapper.User.UpdateAsync(user, id);

            if (user == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return user;
        }

        public async Task<ActionResult<User>> ChangeRole(int id, string role)
        {
            var user = await _repoWrapper.User.FindByIdAsync(id);

            if (user == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            user.Role = role;
            user = await _repoWrapper.User.UpdateAsync(user, id);

            if (user == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return user;
        }
    }
}